using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations;
using EducationSystem.Interfaces;
using EducationSystem.Interfaces.Repositories;
using EducationSystem.Models;
using EducationSystem.Specifications.Basics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EducationSystem.Tests
{
    public class TokenGeneratorTests
    {
        private readonly ITokenGenerator _tokenGenerator;

        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        private readonly Mock<ILogger<TokenGenerator>> _logger = new Mock<ILogger<TokenGenerator>>();

        private readonly Mock<IRepository<DatabaseUser>> _repositoryUser = new Mock<IRepository<DatabaseUser>>();

        public TokenGeneratorTests()
        {
            _tokenGenerator = new TokenGenerator(
                _configuration.Object,
                _logger.Object,
                _repositoryUser.Object);
        }

        [Fact]
        public async Task GenerateToken_UserNotFound()
        {
            _repositoryUser
                .Setup(x => x.FindFirstAsync(It.IsAny<Specification<DatabaseUser>>()))
                .ReturnsAsync((DatabaseUser) null);

            var request = new TokenRequest("email", "password");

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => _tokenGenerator.GenerateTokenAsync(request));
        }

        [Fact]
        public async Task GenerateToken_EmptyData()
        {
            var request = new TokenRequest("", "password");

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => _tokenGenerator.GenerateTokenAsync(request));

            request = new TokenRequest(null, "password");

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => _tokenGenerator.GenerateTokenAsync(request));

            request = new TokenRequest("email", "");

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => _tokenGenerator.GenerateTokenAsync(request));

            request = new TokenRequest("email", null);

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => _tokenGenerator.GenerateTokenAsync(request));
        }

        [Fact]
        public async Task GenerateToken_WrongData()
        {
            var users = new List<DatabaseUser>
            {
                new DatabaseUser { Email = "email", Password = "password" }
            };

            _repositoryUser
                .Setup(x => x.FindFirstAsync(It.IsAny<Specification<DatabaseUser>>()))
                .ReturnsAsync((Specification<DatabaseUser> x) => users.FirstOrDefault(x));

            var request = new TokenRequest("email", "b");

            await Assert.ThrowsAsync<EducationSystemPublicException>
                (() => _tokenGenerator.GenerateTokenAsync(request));

            request = new TokenRequest("b", "password");

            await Assert.ThrowsAsync<EducationSystemNotFoundException>
                (() => _tokenGenerator.GenerateTokenAsync(request));
        }
    }
}