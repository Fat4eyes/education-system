using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Helpers.Implementations.Source;
using EducationSystem.Helpers.Interfaces.Source;
using EducationSystem.Models.Source.Options;
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
        public void IsAdmin_True()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Admin });

            Assert.True(UserHelper.IsAdmin(999));
        }

        [Fact]
        public void IsAdmin_False()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Student });

            Assert.False(UserHelper.IsAdmin(999));
        }

        [Fact]
        public void IsStudent_True()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Student });

            Assert.True(UserHelper.IsStudent(999));
        }

        [Fact]
        public void IsStudent_False()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Lecturer });

            Assert.False(UserHelper.IsStudent(999));
        }

        [Fact]
        public void IsLecturer_True()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Lecturer });

            Assert.True(UserHelper.IsLecturer(999));
        }

        [Fact]
        public void IsLecturer_False()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Employee });

            Assert.False(UserHelper.IsLecturer(999));
        }

        [Fact]
        public void IsEmployee_True()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Employee });

            Assert.True(UserHelper.IsEmployee(999));
        }

        [Fact]
        public void IsEmployee_False()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Admin });

            Assert.False(UserHelper.IsEmployee(999));
        }
    }
}