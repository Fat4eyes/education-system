﻿namespace EducationSystem.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}