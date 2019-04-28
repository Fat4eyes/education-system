﻿using System.Threading.Tasks;
using EducationSystem.Models.Datas;

namespace EducationSystem.Interfaces.Managers
{
    public interface IManagerTestData
    {
        Task<TestData> GetTestDataForStudentByTestId(int testId, int studentId);
    }
}