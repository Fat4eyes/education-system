﻿using EducationSystem.Constants.Source;
using EducationSystem.Database.Models.Source;
using EducationSystem.Exceptions.Source;
using EducationSystem.Implementations.Helpers;
using EducationSystem.Interfaces.Helpers;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Moq;
using Xunit;

namespace EducationSystem.Tests.Source.Helpers
{
    public class TestsHelperUser
    {
        protected IHelperUser HelperUser { get; }

        protected Mock<IRepositoryRole> MockRepositoryRole { get; }

        public TestsHelperUser()
        {
            MockRepositoryRole = new Mock<IRepositoryRole>();

            HelperUser = new HelperUser(MockRepositoryRole.Object);
        }

        [Fact]
        public void CheckRoleStudent_Success()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Student });

            HelperUser.CheckRoleStudent(999);
        }

        [Fact]
        public void CheckRoleStudent_Error()
        {
            MockRepositoryRole
                .Setup(x => x.GetRoleByUserId(999))
                .Returns(new DatabaseRole { Name = UserRoles.Admin });

            Assert.Throws<EducationSystemException>(() => HelperUser.CheckRoleStudent(999));
        }
    }
}