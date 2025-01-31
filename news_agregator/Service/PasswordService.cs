﻿using Microsoft.AspNetCore.Identity;


namespace Service
{
    public class PasswordService
    {
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}

