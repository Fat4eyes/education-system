﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EducationSystem.Database.Models.Source
{
    public class DatabaseModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Column("id")]
        public virtual int Id { get; set; }
    }
}