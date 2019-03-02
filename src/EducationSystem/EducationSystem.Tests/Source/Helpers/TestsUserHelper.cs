using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Helpers.Implementations.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Helpers
{
    public class TestsUserHelper
    {
        protected IUserHelper UserHelper { get; }

        protected Mock<IRepositoryRole> MockRepositoryRole { get; }

        public TestsUserHelper()
        {
            MockRepositoryRole = new Mock<IRepositoryRole>();

            UserHelper = new UserHelper(MockRepositoryRole.Object);
        }

        [Fact]
        public void CheckRoleStudent_Success()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Student });

            UserHelper.CheckRoleStudent(999);
        }

        [Fact]
        public void CheckRoleStudent_Error()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Admin });

            Assert.Throws<EducationSystemException>(() => UserHelper.CheckRoleStudent(999));
        }
    }
}