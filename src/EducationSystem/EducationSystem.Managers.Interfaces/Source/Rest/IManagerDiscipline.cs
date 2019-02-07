using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerDiscipline
    {
        PagedData<Discipline> GetDisciplines(OptionsDiscipline options);

        Discipline GetDisciplineById(int id, OptionsDiscipline options);
    }
}