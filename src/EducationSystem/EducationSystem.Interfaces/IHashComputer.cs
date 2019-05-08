using System.Threading.Tasks;
using EducationSystem.Database.Models;

namespace EducationSystem.Interfaces
{
    public interface IHashComputer
    {
        string Compute(object @object);

        Task<string> ComputeForQuestionAsync(DatabaseQuestion question);
    }
}