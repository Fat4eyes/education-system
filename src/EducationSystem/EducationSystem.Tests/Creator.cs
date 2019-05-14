using System.Collections.Generic;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Models.Rest;

namespace EducationSystem.Tests
{
    public static class Creator
    {
        #region User

        public static User CreateAdmin()
        {
            return new User
            {
                Id = 1,
                Roles = new List<Role>
                {
                    CreateRole(UserRoles.Admin)
                }
            };
        }

        public static User CreateLecturer()
        {
            return new User
            {
                Id = 2,
                Roles = new List<Role>
                {
                    CreateRole(UserRoles.Lecturer)
                }
            };
        }

        public static User CreateStudent()
        {
            return new User
            {
                Id = 3,
                Roles = new List<Role>
                {
                    CreateRole(UserRoles.Student)
                }
            };
        }

        public static User CreateEmployee()
        {
            return new User
            {
                Id = 4,
                Roles = new List<Role>
                {
                    CreateRole(UserRoles.Employee)
                }
            };
        }

        #endregion

        #region DatabaseUser

        public static DatabaseUser CreateDatabaseAdmin()
        {
            return new DatabaseUser
            {
                Id = 1,
                UserRoles = new List<DatabaseUserRole>
                {
                    new DatabaseUserRole
                    {
                        Role = CreateDatabaseRole(UserRoles.Admin)
                    }
                }
            };
        }

        public static DatabaseUser CreateDatabaseLecturer()
        {
            return new DatabaseUser
            {
                Id = 2,
                UserRoles = new List<DatabaseUserRole>
                {
                    new DatabaseUserRole
                    {
                        Role = CreateDatabaseRole(UserRoles.Lecturer)
                    }
                }
            };
        }

        public static DatabaseUser CreateDatabaseStudent()
        {
            return new DatabaseUser
            {
                Id = 3,
                UserRoles = new List<DatabaseUserRole>
                {
                    new DatabaseUserRole
                    {
                        Role = CreateDatabaseRole(UserRoles.Student)
                    }
                }
            };
        }

        #endregion

        #region DatabaseDiscipline

        public static List<DatabaseDiscipline> CreateDisciplines()
        {
            return new List<DatabaseDiscipline>
            {
                CreateDiscipline(),
                CreateDiscipline(),
                CreateDiscipline(9, 9),
                CreateDiscipline(9)
            };
        }

        public static DatabaseDiscipline CreateDiscipline(int studentId = 3, int lecturerId = 2)
        {
            return new DatabaseDiscipline
            {
                StudyProfiles = new List<DatabaseStudyProfileDiscipline>
                {
                    new DatabaseStudyProfileDiscipline
                    {
                        StudyProfile = CreateDatabaseStudyProfile(studentId)
                    }
                },
                Lecturers = new List<DatabaseDisciplineLecturer>
                {
                    new DatabaseDisciplineLecturer
                    {
                        LecturerId = lecturerId
                    }
                }
            };
        }

        #endregion

        #region DatabaseStudyProfile

        public static DatabaseStudyProfile CreateDatabaseStudyProfile(int studentId)
        {
            return new DatabaseStudyProfile
            {
                StudyPlans = new List<DatabaseStudyPlan>
                {
                    CreateDatabaseStudyPlan(studentId)
                }
            };
        }

        #endregion

        #region DatabaseStudyPlan

        public static DatabaseStudyPlan CreateDatabaseStudyPlan(int studentId)
        {
            return new DatabaseStudyPlan
            {
                Groups = new List<DatabaseGroup>
                {
                    CreateDatabaseGroup(studentId)
                }
            };
        }

        #endregion

        #region DatabaseGroup

        public static DatabaseGroup CreateDatabaseGroup(int studentId)
        {
            return new DatabaseGroup
            {
                GroupStudents = new List<DatabaseStudentGroup>
                {
                    new DatabaseStudentGroup
                    {
                        StudentId = studentId
                    }
                }
            };
        }

        #endregion

        public static Role CreateRole(string name) => new Role { Name = name };

        public static DatabaseRole CreateDatabaseRole(string name) => new DatabaseRole { Name = name };
    }
}