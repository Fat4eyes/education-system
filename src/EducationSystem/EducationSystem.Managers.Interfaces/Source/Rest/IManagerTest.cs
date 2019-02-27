﻿using EducationSystem.Models.Source;
using EducationSystem.Models.Source.Filters;
using EducationSystem.Models.Source.Options;
using EducationSystem.Models.Source.Rest;

namespace EducationSystem.Managers.Interfaces.Source.Rest
{
    public interface IManagerTest
    {
        PagedData<Test> GetTests(OptionsTest options, FilterTest filter);
        PagedData<Test> GetTestsByDisciplineId(int disciplineId, OptionsTest options, FilterTest filter);
        PagedData<Test> GetTestsByStudentId(int studentId, OptionsTest options, FilterTest filter);

        Test GetTestById(int id, OptionsTest options);

        void DeleteTestById(int id);
    }
}