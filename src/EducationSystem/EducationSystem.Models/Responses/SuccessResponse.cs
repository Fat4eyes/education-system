namespace EducationSystem.Models.Responses
{
    public class SuccessResponse
    {
        public bool Success => true;

        public object Data { get; }

        public SuccessResponse(object data)
        {
            Data = data;
        }
    }
}