using EducationSystem.Enums;

namespace EducationSystem.Models.Filters
{
    public class FilterFile : Filter
    {
        public FileType? Type { get; set; }

        public FilterFile SetFileType(FileType type)
        {
            Type = type;
            return this;
        }
    }
}