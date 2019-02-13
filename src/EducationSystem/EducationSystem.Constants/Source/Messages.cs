namespace EducationSystem.Constants.Source
{
    public static class Messages
    {
        public static readonly string InternalServerError =
            "Внутренняя ошибка сервера. Попробуйте повторить операцию еще раз. " +
            "Если ошибка будет повторяться, пожалуйста, обратитесь к администратору.";

        public static readonly string TokenError =
            "Неверная электронная почта или пароль.";

        public static class Student
        {
            public static string NotFoundById(object parameter)
                => string.Format(TemplteNotFoundById, parameter);

            public static readonly string TemplteNotFoundById =
                "Студент не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Студент не найден.";
        }

        public static class User
        {
            public static string NotStudent(object parameter)
                => string.Format(TemplateNotStudent, parameter);

            public static readonly string TemplateNotStudent =
                "Пользователь не является студентом. Идентификатор пользователя: {0}.";

            public static readonly string NotStudentPublic =
                "Пользователь не является студентом.";

            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Пользователь не найден. Идентификатор: {0}.";

            public static string NotFoundByEmail(object parameter)
                => string.Format(TemplateNotFoundByEmail, parameter);

            public static readonly string TemplateNotFoundByEmail =
                "Пользователь не найден. Электронная почта: {0}";

            public static readonly string NotFoundPublic =
                "Пользователь не найден.";
        }

        public static class Discipline
        {
            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Дисциплина не найдена. Идентификатор дисциплины: {0}.";

            public static readonly string NotFoundPublic =
                "Дисциплина не найдена.";
        }

        public static class Group
        {
            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Группа не найдена. Идентификатор группы: {0}.";

            public static string NotFoundByStudentId(object parameter)
                => string.Format(TemplateNotFoundByStudentId, parameter);

            public static readonly string TemplateNotFoundByStudentId =
                "Группа не найдена. Идентификатор студента (пользователя): {0}.";

            public static readonly string NotFoundPublic =
                "Группа не найдена.";
        }

        public static class Institute
        {
            public static string NotFoundByStuentId(object parameter)
                => string.Format(TemplateNotFoundByStuentId, parameter);

            public static readonly string TemplateNotFoundByStuentId =
                "Институт не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Дисциплина не найдена.";
        }

        public static class Role
        {
            public static string NotFoundByUserId(object parameter)
                => string.Format(TemplateNotFoundByUserId, parameter);

            public static readonly string TemplateNotFoundByUserId =
                "Роль не найдена. Идентификатор пользователя: {0}.";

            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Роль не найдена. Идентификатор роли: {0}.";

            public static readonly string NotFoundPublic =
                "Роль не найдена.";
        }

        public static class StudyPlan
        {
            public static string NotFoundByStuentId(object parameter)
                => string.Format(TemplateNotFoundByStuentId, parameter);

            public static readonly string TemplateNotFoundByStuentId =
                "Учебный план не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Учебный план не найден.";
        }

        public static class StudyProfile
        {
            public static string NotFoundByStuentId(object parameter)
                => string.Format(TemplateNotFoundByStuentId, parameter);

            public static readonly string TemplateNotFoundByStuentId =
                "Профиль обучения не найден. Идентификатор студента: {0}.";

            public static readonly string NotFoundPublic =
                "Профиль обучения не найден.";
        }

        public static class Test
        {
            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Тест не найден. Идентификатор теста: {0}.";

            public static readonly string NotFoundPublic =
                "Тест не найден.";
        }

        public static class TestResult
        {
            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Результат теста не найден. Идентификатор: {0}.";

            public static readonly string NotFoundPublic =
                "Результат теста не найден.";
        }

        public static class Theme
        {
            public static string NotFoundById(object parameter)
                => string.Format(TemplateNotFoundById, parameter);

            public static readonly string TemplateNotFoundById =
                "Тема не найдена. Идентификатор темы: {0}.";

            public static readonly string NotFoundPublic =
                "Тема не найдена.";
        }
    }
}