using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryMaterialFile : Repository<DatabaseMaterialFile>, IRepositoryMaterialFile
    {
        public RepositoryMaterialFile(DatabaseContext context)
            : base(context) { }
    }
}