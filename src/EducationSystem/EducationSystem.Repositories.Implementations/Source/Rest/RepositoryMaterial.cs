using EducationSystem.Database.Models.Source;
using EducationSystem.Database.Source.Contexts;
using EducationSystem.Repositories.Interfaces.Source.Rest;

namespace EducationSystem.Repositories.Implementations.Source.Rest
{
    public class RepositoryMaterial : Repository<DatabaseMaterial>, IRepositoryMaterial
    {
        public RepositoryMaterial(DatabaseContext context)
            : base(context) { }
    }
}