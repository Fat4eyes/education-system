namespace EducationSystem.Models.Source.Rest
{
    public class UserWithGroupAndStudyPlan : UserWithGroup
    {
        public new GroupWithStudyPlan Group { get; set; }
    }
}