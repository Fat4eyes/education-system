using EducationSystem.Models;
using EducationSystem.Models.Filters;
using EducationSystem.Models.Options;
using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Interfaces.Managers.Rest
{
    public interface IManagerDiscipline
    {
        PagedData<Discipline> GetDisciplines(OptionsDiscipline options, FilterDiscipline filter);
        PagedData<Discipline> GetDisciplinesForStudent(int studentId, OptionsDiscipline options, FilterDiscipline filter);

        Discipline GetDisciplineById(int id, OptionsDiscipline options);
    }
}