using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Exceptions.Helpers;
using EducationSystem.Interfaces.Managers;
using EducationSystem.Models;
using EducationSystem.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Crypt = BCrypt.Net.BCrypt;

namespace EducationSystem.Implementations.Managers
{
    public sealed class ManagerToken : Manager<ManagerToken>, IManagerToken
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryUser _repositoryUser;

        public ManagerToken(
            IMapper mapper,
            ILogger<ManagerToken> logger,
            IConfiguration configuration,
            IRepositoryUser repositoryUser)
            : base(mapper, logger)
        {
            _configuration = configuration;
            _repositoryUser = repositoryUser;
        }

        public async Task<TokenResponse> GenerateTokenAsync(TokenRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw ExceptionHelper.CreatePublicException("Неверная электронная почта или пароль.");

            var user = await _repositoryUser.GetUserByEmailAsync(request.Email) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Пользователь не найден. Электронная почта: {request.Email}",
                    $"Неверная электронная почта или пароль.");

            if (!Crypt.Verify(request.Password, user.Password))
            {
                Logger.LogError($"Пользователь найден, но пароль указан неверно. " +
                                $"Идентификатор пользователя: {user.Id}.");

                throw ExceptionHelper.CreatePublicException("Неверная электронная почта или пароль.");
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

            var tokenParameters = _configuration
                .GetSection(nameof(TokenParameters))
                .Get<TokenParameters>();

            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromMinutes(tokenParameters.LifeTimeInMinutes));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenParameters.SecretKeyInBytes),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                tokenParameters.Issuer,
                tokenParameters.Audience,
                identity.Claims, now, expires, signingCredentials);

            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}