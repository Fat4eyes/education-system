using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerDiscipline
    {
        PagedData<Discipline> GetDisciplines(OptionsDiscipline options, Filter filter);
        PagedData<Discipline> GetDisciplinesByStudentId(int studentId, OptionsDiscipline options, Filter filter);

        Discipline GetDisciplineById(int id, OptionsDiscipline options);
    }
}