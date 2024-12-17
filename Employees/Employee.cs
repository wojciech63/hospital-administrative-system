using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Project_1_OOP_Wojciech_Dabrowski.Employees
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PESEL { get; set; }
        public string Username { get; set; }
        private string PasswordHash { get; set; }

        public Employee(string name, string surname, int pesel, string username, string password)
        {
            Name = name;
            Surname = surname;
            PESEL = pesel;
            Username = username;
            SetPassword(password);
        }
        public void SetPassword(string password)
        {
            PasswordHash = HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
