using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Implementations.Basics;
using EducationSystem.Repositories.Interfaces;

namespace EducationSystem.Repositories.Implementations
{
    public class RepositoryMaterialFile : Repository<DatabaseMaterialFile>, IRepositoryMaterialFile
    {
        public RepositoryMaterialFile(DatabaseContext context)
            : base(context) { }
    }
}