using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using EducationSystem.Enums.Source;
using EducationSystem.Exceptions.Source.Helpers;
using EducationSystem.Extensions.Source;
using EducationSystem.Managers.Interfaces.Source.Export;
using EducationSystem.Models.Source.Export;
using EducationSystem.Repositories.Interfaces.Source.Rest;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using File = EducationSystem.Models.Source.Files.File;

namespace EducationSystem.Managers.Implementations.Source.Export
{
    public class ManagerExportQuestion : ManagerExport, IManagerExportQuestion
    {
        private readonly IRepositoryTheme _repositoryTheme;

        public ManagerExportQuestion(
            IMapper mapper,
            ILogger<ManagerExportQuestion> logger,
            IRepositoryTheme repositoryTheme)
            : base(mapper, logger)
        {
            _repositoryTheme = repositoryTheme;
        }

        public File ExportThemeQuestions(int themeId)
        {
            var theme = _repositoryTheme.GetById(themeId) ??
                throw ExceptionHelper.CreateNotFoundException(
                    $"Тема не найдена. Идентификатор темы: {themeId}.",
                    $"Тема не найдена.");

            if (theme.Questions.IsEmpty())
                throw ExceptionHelper.CreatePublicException("Тема не содержит вопросы.");

            if (theme.Questions.Any(x => x.Type == QuestionType.WithProgram))
                Logger.LogInformation("Тема содержит вопросы с программным кодом. При экспорте они будут проигнорированы.");

            var questions = theme.Questions
                .Where(x => x.Type != QuestionType.WithProgram)
                .ToList();

            var items = Mapper.Map<List<ExportQuestion>>(questions);
            var json = JsonConvert.SerializeObject(items);
            var bytes = Encoding.UTF8.GetBytes(json);

            return new File("questions.json", new MemoryStream(bytes));
        }
    }
}