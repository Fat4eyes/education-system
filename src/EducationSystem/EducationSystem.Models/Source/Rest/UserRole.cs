﻿namespace EducationSystem.Models.Source.Rest
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class UserRole : UserRoleShort
    {
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уровень.
        /// </summary>
        public int Level { get; set; }
    }
}