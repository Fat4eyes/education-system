using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Crypt = BCrypt.Net.BCrypt;

namespace EducationSystem.Managers.Implementations.Source
{
    public class ManagerToken : Manager<ManagerToken>, IManagerToken
    {
        protected IRepositoryUser RepositoryUser { get; }

        public ManagerToken(
            IMapper mapper,
            ILogger<ManagerToken> logger,
            IRepositoryUser repositoryUser)
            : base(mapper, logger)
        {
            RepositoryUser = repositoryUser;
        }

        public TokenResponse GenerateToken(TokenRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new EducationSystemPublicException("Не указана электронная почта.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new EducationSystemPublicException("Не указан пароль.");

            var user = RepositoryUser.GetUserByEmail(request.Email, OptionsUser.IncludeRoles) ??
                throw new EducationSystemNotFoundException(
                    $"Пользователь не найден. Электронная почта: {request.Email}.",
                    new EducationSystemPublicException("Неверная электронная почта или пароль."));

            if (!Crypt.Verify(request.Password, user.Password))
            {
                Logger.LogError($"Пользователь найден, но пароль указан неверно. " +
                                $"Идентификатор пользователя: {user.Id}.");

                throw new EducationSystemPublicException("Неверная электронная почта или пароль.");
            }

            var claims = new List<Claim> {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            claims.AddRange(user.UserRoles.Select(x =>
                new Claim(ClaimsIdentity.DefaultRoleClaimType, x.Role.Name)));

            var identity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromMinutes(90));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(TokenParameters.SecretKeyInBytes),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                TokenParameters.Issuer,
                TokenParameters.Audience,
                identity.Claims, now, expires, signingCredentials);

            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}