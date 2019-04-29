using System.Threading.Tasks;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Exceptions;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Repositories.Interfaces;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Helpers
{
    public class TestsHelperUser
    {
        private readonly IHelperUserRole _helperUserRole;

        private readonly Mock<IRepositoryRole> _mockRepositoryRole
            = new Mock<IRepositoryRole>();

        public TestsHelperUser()
        {
            _helperUserRole = new HelperUserRole(_mockRepositoryRole.Object);
        }

        [Fact]
        public void CheckRoleStudent_Success()
        {
            _mockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .ReturnsAsync(new DatabaseRole { Name = UserRoles.Student });

            _helperUserRole.CheckRoleStudent(999);
        }

        [Theory]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.Lecturer)]
        public async Task CheckRoleStudent_Error(string role)
        {
            _mockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .ReturnsAsync(new DatabaseRole { Name = role });

            await Assert.ThrowsAsync<EducationSystemException>(() => _helperUserRole.CheckRoleStudent(999));
        }
    }
}