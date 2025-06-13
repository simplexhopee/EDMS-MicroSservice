using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Auth
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private static readonly Random Random = new Random();
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string SpecialCharacters = "@$!%*?&";
        private const int PasswordLength = 12;

        public string GenerateRandomPassword()
        {
            StringBuilder password = new StringBuilder();
            password.Append(Uppercase[Random.Next(Uppercase.Length)]);
            password.Append(Digits[Random.Next(Digits.Length)]);
            password.Append(SpecialCharacters[Random.Next(SpecialCharacters.Length)]);

            string allCharacters = Uppercase + Lowercase + Digits + SpecialCharacters;
            for (int i = 3; i < PasswordLength; i++)
            {
                password.Append(allCharacters[Random.Next(allCharacters.Length)]);
            }

            return new string(password.ToString().ToCharArray().OrderBy(x => Random.Next()).ToArray());
        }
    }
}
