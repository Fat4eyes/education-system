using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Extensions;
using EducationSystem.Helpers;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models;
using EducationSystem.Specifications.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Crypt = BCrypt.Net.BCrypt;

namespace EducationSystem.Implementations
{
    public sealed class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenGenerator> _logger;
        private readonly IRepository<DatabaseUser> _repositoryUser;

        private const string PublicExceptionMessage = "Неверная электронная почта или пароль.";

        public TokenGenerator(
            IConfiguration configuration,
            ILogger<TokenGenerator> logger,
            IRepository<DatabaseUser> repositoryUser)
        {
            _logger = logger;
            _configuration = configuration;
            _repositoryUser = repositoryUser;
        }

        public async Task<TokenResponse> GenerateTokenAsync(TokenRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw ExceptionHelper.CreatePublicException(PublicExceptionMessage);

            var user = await _repositoryUser.FindFirstAsync(new UsersByEmail(request.Email)) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Пользователь не найден. Электронная почта: {request.Email}.",
                    PublicExceptionMessage);

            var role = user.UserRoles
                .Select(x => x.Role.Name)
                .FirstOrDefault() ??
                    throw ExceptionHelper.CreatePublicException("Не удалось получить роль пользователя.");

            if (UserRoles.All.ToLowerInvariant().Contains(role.ToLowerInvariant()) == false)
                throw ExceptionHelper.CreatePublicException("Роль пользователя не поддерживается системой.");

            if (!Crypt.Verify(request.Password, user.Password))
            {
                _logger.LogInformation(
                    $"Пользователь найден, но пароль указан неверно. " +
                    $"Идентификатор пользователя: {user.Id}.");

                throw ExceptionHelper.CreatePublicException(PublicExceptionMessage);
            }

            if (user.Active == false)
                throw ExceptionHelper.CreatePublicException("Аккаунт не подтверждён администратором.");

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

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