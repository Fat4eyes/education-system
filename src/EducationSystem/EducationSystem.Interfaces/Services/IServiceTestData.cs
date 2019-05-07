using System.Collections.Generic;
using System.Threading.Tasks;
using EducationSystem.Models.Datas;

namespace EducationSystem.Interfaces.Services
{
    public interface IServiceTestData
    {
        Task<List<TestData>> GetTestsDataAsync(int[] ids);
    }
}