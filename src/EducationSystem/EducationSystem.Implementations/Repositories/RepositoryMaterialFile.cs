using EducationSystem.Database.Contexts;
using EducationSystem.Database.Models;
using EducationSystem.Implementations.Repositories.Basics;
using EducationSystem.Interfaces.Repositories;

namespace EducationSystem.Implementations.Repositories
{
    public class RepositoryMaterialFile : Repository<DatabaseMaterialFile>, IRepositoryMaterialFile
    {
        public RepositoryMaterialFile(DatabaseContext context) : base(context) { }
    }
}