using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using EducationSystem.Constants.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Managers.Implementations.Source.Base;
using EducationSystem.Managers.Interfaces.Source;
using EducationSystem.Managers.Interfaces.Source.Rest;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EducationSystem.Managers.Implementations.Source
{
    public class AuthManager : Manager<AuthManager>, IAuthManager
    {
        protected IUserManager UserManager { get; }

        public AuthManager(
            IMapper mapper,
            ILogger<AuthManager> logger,
            IUserManager userManager)
            : base(mapper, logger)
        {
            UserManager = userManager;
        }

        /// <inheritdoc />
        public SignInResponse SignIn(SignInRequest model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var user = UserManager.GetByEmailAndPassword(model.Email, model.Password) ??
                throw new EducationSystemNotFoundException(
                    $"Пользователь не найден. Электронная почта: {model.Email}.",
                    new EducationSystemPublicException("Неверная электронная почта или пароль."));

            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            claims.AddRange(user.Roles.Select(x =>
                new Claim(ClaimsIdentity.DefaultRoleClaimType, x.Name)));

            var identity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var expires = model.Remember
                ? now.Add(TimeSpan.FromDays(7))
                : now.Add(TimeSpan.FromMinutes(60));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(TokenParameters.SecretKeyInBytes),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                TokenParameters.Issuer,
                TokenParameters.Audience,
                identity.Claims, now, expires, signingCredentials);

            return new SignInResponse {
                User = Mapper.Map<UserShort>(user),
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}