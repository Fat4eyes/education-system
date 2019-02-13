namespace EducationSystem.Constants.Source
{
    public static class Messages
    {
        public static readonly string InternalServerError =
            "Внутренняя ошибка сервера. Попробуйте повторить операцию еще раз. " +
            "Если ошибка будет повторяться, пожалуйста, обратитесь к администратору.";

        public static class Student
        {
            public static readonly string NotFoundById =
                "Студент не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Студент не найден.";
        }

        public static class User
        {
            public static readonly string NotStudent =
                "Пользователь не является студентом. Идентификатор пользователя: {0}.";

            public static readonly string NotStudentPublic =
                "Пользователь не является студентом.";

            public static readonly string NotFoundById =
                "Пользователь не найден. Идентификатор: {0}.";

            public static readonly string NotFoundPublic =
                "Пользователь не найден.";
        }

        public static class Discipline
        {
            public static readonly string NotFoundById =
                "Дисциплина не найдена. Идентификатор дисциплины: {0}.";

            public static readonly string NotFoundPublic =
                "Дисциплина не найдена.";
        }

        public static class Group
        {
            public static readonly string NotFoundById =
                "Группа не найдена. Идентификатор группы: {0}.";

            public static readonly string NotFoundByStudentId =
                "Группа не найдена. Идентификатор студента (пользователя): {0}.";

            public static readonly string NotFoundPublic =
                "Группа не найдена.";
        }

        public static class Institute
        {
            public static readonly string NotFoundByStuentId =
                "Институт не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Дисциплина не найдена.";
        }

        public static class Role
        {
            public static readonly string NotFoundByUserId =
                "Роль не найдена. Идентификатор пользователя: {0}.";

            public static readonly string NotFoundById =
                "Роль не найдена. Идентификатор роли: {0}.";

            public static readonly string NotFoundPublic =
                "Роль не найдена.";
        }

        public static class StudyPlan
        {
            public static readonly string NotFoundByStuentId =
                "Учебный план не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Учебный план не найден.";
        }

        public static class StudyProfile
        {
            public static readonly string NotFoundByStuentId =
                "Профиль обучения не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Профиль обучения не найден.";
        }

        public static class Test
        {
            public static readonly string NotFoundById =
                "Тест не найден. Идентификатор теста: {0}.";

            public static readonly string NotFoundPublic =
                "Тест не найден.";
        }

        public static class TestResult
        {
            public static readonly string NotFoundById =
                "Результат теста не найден. Идентификатор: {0}.";

            public static readonly string NotFoundPublic =
                "Результат теста не найден.";
        }

        public static class Theme
        {
            public static readonly string NotFoundById =
                "Тема не найдена. Идентификатор темы: {0}.";

            public static readonly string NotFoundPublic =
                "Тема не найдена.";
        }
    }
}