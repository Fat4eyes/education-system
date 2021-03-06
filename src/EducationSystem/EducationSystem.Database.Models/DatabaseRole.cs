﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Basics;

namespace EducationSystem.Database.Models
{
    [Table("roles")]
    public class DatabaseRole : DatabaseModel
    {
        [Column("name")]
        public virtual string Name { get; set; }

        [Column("slug")]
        public virtual string Slug { get; set; }

        [Column("description")]
        public virtual string Description { get; set; }

        [Column("level")]
        public virtual int Level { get; set; }

        public virtual List<DatabaseUserRole> RoleUsers { get; set; }
            = new List<DatabaseUserRole>();
    }
}