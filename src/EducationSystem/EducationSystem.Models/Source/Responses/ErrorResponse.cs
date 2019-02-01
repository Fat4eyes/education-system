﻿namespace EducationSystem.Models.Source.Responses
{
    public class ErrorResponse
    {
        public bool Success => false;

        public string Error { get; }

        public ErrorResponse(string erorr)
        {
            Error = erorr;
        }
    }
}