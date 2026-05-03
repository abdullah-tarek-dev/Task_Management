using Microsoft.AspNetCore.Identity;
using Task_Management.Models;

namespace Task_Management.Auth
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _hasher ;
        public PasswordService()
        {
            _hasher = new PasswordHasher<User>();
        }

        public string HashPassword(User user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string password, string hashedPassword)
        {
            return _hasher.VerifyHashedPassword(user, hashedPassword, password)
                == PasswordVerificationResult.Success;
        }
    }
}
