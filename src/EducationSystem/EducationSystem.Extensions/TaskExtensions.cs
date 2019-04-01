using System.Threading.Tasks;

namespace EducationSystem.Extensions
{
    public static class TaskExtensions
    {
        public static void WaitTask(this Task task)
            => task.GetAwaiter().GetResult();

        public static T WaitTask<T>(this Task<T> task)
            => task.GetAwaiter().GetResult();
    }
}