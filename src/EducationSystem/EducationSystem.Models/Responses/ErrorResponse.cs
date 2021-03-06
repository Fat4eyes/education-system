﻿namespace EducationSystem.Models.Responses
{
    public class ErrorResponse
    {
        public bool Success => false;

        public string Error { get; }

        public ErrorResponse(string error)
        {
            Error = error;
        }
    }
}