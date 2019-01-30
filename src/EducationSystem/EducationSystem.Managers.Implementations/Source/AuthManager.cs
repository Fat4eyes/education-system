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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EducationSystem.Managers.Implementations.Source
{
    public class AuthManager : Manager<AuthManager>, IAuthManager
    {
        protected IManagerUser ManagerUser { get; }

        public AuthManager(
            IMapper mapper,
            ILogger<AuthManager> logger,
            IManagerUser managerUser)
            : base(mapper, logger)
        {
            ManagerUser = managerUser;
        }

        public SignInResponse SignIn(SignInRequest model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var user = ManagerUser.GetByEmailAndPassword(model.Email, model.Password) ??
                throw new EducationSystemNotFoundException($"Пользователь не найден. Электронная почта: {model.Email}.");

            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            var roles = user.Roles
                .Select(x => x.Name)
                .ToList();

            claims.AddRange(roles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x)));

            var identity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var token = CreateToken(model, identity);

            return new SignInResponse {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = identity.Name,
                Roles = roles
            };
        }

        private static JwtSecurityToken CreateToken(SignInRequest model, ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var expires = model.Remember
                ? now.Add(TimeSpan.FromDays(7))
                : now.Add(TimeSpan.FromMinutes(60));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(TokenParameters.SecretKeyInBytes),
                SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                TokenParameters.Issuer,
                TokenParameters.Audience,
                identity.Claims, now, expires, signingCredentials);
        }
    }
}