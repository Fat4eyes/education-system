using System.Collections.Generic;
using EducationSystem.Constants;
using EducationSystem.Database.Models;
using EducationSystem.Enums;
using EducationSystem.Models.Rest;

namespace EducationSystem.Tests.Helpers
{
    public static class ModelsCreationHelper
    {
        #region User

        public static User CreateAdmin()
        {
            return new User { Id = 1, Roles = CreateRoles(UserRoles.Admin) };
        }

        public static User CreateLecturer()
        {
            return new User { Id = 2, Roles = CreateRoles(UserRoles.Lecturer) };
        }

        public static User CreateStudent()
        {
            return new User { Id = 3, Roles = CreateRoles(UserRoles.Student) };
        }

        #endregion

        #region DatabaseStudyProfileDiscipline

        public static List<DatabaseStudyProfileDiscipline> CreateDatabaseStudyProfileDisciplines(int studentId = 3)
        {
            return new List<DatabaseStudyProfileDiscipline> { CreateDatabaseStudyProfileDiscipline(studentId) };
        }

        public static DatabaseStudyProfileDiscipline CreateDatabaseStudyProfileDiscipline(int studentId = 3)
        {
            return new DatabaseStudyProfileDiscipline { StudyProfile = CreateDatabaseStudyProfile(studentId) };
        }

        #endregion

        #region DatabaseStudyProfileDiscipline

        public static List<DatabaseDisciplineLecturer> CreateDatabaseDisciplineLecturers(int lecturerId = 2)
        {
            return new List<DatabaseDisciplineLecturer> { CreateDatabaseDisciplineLecturer(lecturerId) };
        }

        public static DatabaseDisciplineLecturer CreateDatabaseDisciplineLecturer(int lecturerId = 2)
        {
            return new DatabaseDisciplineLecturer { LecturerId = lecturerId };
        }

        #endregion

        #region DatabaseDiscipline

        public static DatabaseDiscipline CreateDatabaseDiscipline(int studentId = 3, int lecturerId = 2)
        {
            return new DatabaseDiscipline
            {
                StudyProfiles = CreateDatabaseStudyProfileDisciplines(studentId),
                Lecturers = CreateDatabaseDisciplineLecturers(lecturerId)
            };
        }

        #endregion

        #region DatabaseStudyProfile

        public static DatabaseStudyProfile CreateDatabaseStudyProfile(int studentId)
        {
            return new DatabaseStudyProfile { StudyPlans = CreateDatabaseStudyPlans(studentId) };
        }

        #endregion

        #region DatabaseStudyPlan

        public static List<DatabaseStudyPlan> CreateDatabaseStudyPlans(int studentId)
        {
            return new List<DatabaseStudyPlan> { CreateDatabaseStudyPlan(studentId) };
        }

        public static DatabaseStudyPlan CreateDatabaseStudyPlan(int studentId)
        {
            return new DatabaseStudyPlan { Groups = CreateDatabaseGroups(studentId) };
        }

        #endregion

        #region DatabaseStudentGroup

        public static List<DatabaseStudentGroup> CreateDatabaseStudentGroups(int studentId)
        {
            return new List<DatabaseStudentGroup> { CreateDatabaseStudentGroup(studentId) };
        }

        public static DatabaseStudentGroup CreateDatabaseStudentGroup(int studentId)
        {
            return new DatabaseStudentGroup { StudentId = studentId };
        }

        #endregion

        #region DatabaseGroup

        public static List<DatabaseGroup> CreateDatabaseGroups(int studentId)
        {
            return new List<DatabaseGroup> { CreateDatabaseGroup(studentId) };
        }

        public static DatabaseGroup CreateDatabaseGroup(int studentId)
        {
            return new DatabaseGroup { GroupStudents = CreateDatabaseStudentGroups(studentId) };
        }

        #endregion

        #region DatabaseMaterial

        public static DatabaseMaterial CreateDatabaseMaterial(int ownerId)
        {
            return new DatabaseMaterial { OwnerId = ownerId };
        }

        #endregion

        #region DatabaseQuestion

        public static DatabaseQuestion CreateDatabaseQuestion(int studentId = 3, int lecturerId = 2, bool withTheme = true)
        {
            var question = new DatabaseQuestion { Type = QuestionType.ClosedManyAnswers };

            if (withTheme)
                question.Theme = CreateDatabaseTheme(studentId, lecturerId);

            return question;
        }

        #endregion

        #region DatabaseTheme

        public static DatabaseTheme CreateDatabaseTheme(int studentId = 3, int lecturerId = 2)
        {
            return new DatabaseTheme
            {
                Discipline = CreateDatabaseDiscipline(studentId, lecturerId),
                Questions = new List<DatabaseQuestion> {
                    CreateDatabaseQuestion(studentId, lecturerId, false),
                    CreateDatabaseQuestion(studentId, lecturerId, false),
                    CreateDatabaseQuestion(studentId, lecturerId, false)
                }
            };
        }

        #endregion

        #region DatabaseTest

        public static DatabaseTest CreateDatabaseTest(bool isActive = false, int studentId = 2, int lecturerId = 2)
        {
            return new DatabaseTest
            {
                IsActive = isActive,
                Discipline = CreateDatabaseDiscipline(studentId, lecturerId),
                TestThemes = CreateDatabaseTestThemes(studentId, lecturerId)
            };
        }

        #endregion

        #region DatabaseTestTheme

        public static List<DatabaseTestTheme> CreateDatabaseTestThemes(int studentId = 3, int lecturerId = 3)
        {
            return new List<DatabaseTestTheme> { CreateDatabaseTestTheme(studentId, lecturerId) };
        }

        public static DatabaseTestTheme CreateDatabaseTestTheme(int studentId = 3, int lecturerId = 3)
        {
            return new DatabaseTestTheme { Theme = CreateDatabaseTheme(studentId, lecturerId) };
        }

        #endregion

        #region Role

        public static List<Role> CreateRoles(string name)
        {
            return new List<Role> { CreateRole(name) };
        }

        public static Role CreateRole(string name)
        {
            return new Role { Name = name };
        }

        #endregion

        #region DatabaseUserRole

        public static List<DatabaseUserRole> CreateDatabaseUserRoles(string name)
        {
            return new List<DatabaseUserRole> { CreateDatabaseUserRole(name) };
        }

        public static DatabaseUserRole CreateDatabaseUserRole(string name)
        {
            return new DatabaseUserRole { Role = CreateDatabaseRole(name) };
        }

        #endregion

        #region DatabaseRole

        public static List<DatabaseRole> CreateDatabaseRoles(string name)
        {
            return new List<DatabaseRole> { CreateDatabaseRole(name) };
        }

        public static DatabaseRole CreateDatabaseRole(string name)
        {
            return new DatabaseRole { Name = name };
        }

        #endregion
    }
}