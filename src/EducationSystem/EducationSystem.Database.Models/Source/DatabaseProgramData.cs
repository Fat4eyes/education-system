using System.ComponentModel.DataAnnotations.Schema;
using EducationSystem.Database.Models.Source.Base;

namespace EducationSystem.Database.Models.Source
{
    [Table("params_set")]
    public class DatabaseProgramData : DatabaseModel
    {
        [Column("program_id")]
        public virtual int ProgramId { get; set; }

        [ForeignKey(nameof(ProgramId))]
        public virtual DatabaseProgram Program { get; set; }

        [Column("input")]
        public virtual string Input { get; set; }

        [Column("expected_output")]
        public virtual string ExpectedOutput { get; set; }
    }
}