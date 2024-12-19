using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Project_1_OOP_Wojciech_Dabrowski.Employees
{
    public abstract class Employee
    {
        public enum Role
        {
            Doctor,
            Nurse,
            Administrator
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int PESEL { get; set; }
        public string Username { get; set; }
        private string PasswordHash { get; set; }
        public Role UserRole { get; set; }

        public Employee(string name, string surname, int pesel, string username, string password, Role userRole)
        {
            Name = name;
            Surname = surname;
            PESEL = pesel;
            Username = username;
            SetPassword(password);
            UserRole = userRole;
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

        public static Employee? Login(string username, string password, List<Employee> employees)
        {
            var employee = employees.Find(e => e.Username == username);
            if (employee != null && employee.VerifyPassword(password))
            {
                return employee;
            }
            else
            {
                Console.WriteLine("Invalid username or password");
                return null;
            }
        }
    }
}
