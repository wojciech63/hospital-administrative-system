using System;
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
        private string Surname { get; set; }
        private int PESEL { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private Role UserRole { get; set; }

        public Employee(string name, string surname, int pesel, string username, string password, Role userRole)
        {
            Name = name;
            Surname = surname;
            PESEL = pesel;
            Username = username;
            Password = password;
            UserRole = userRole;
        }

        public static Employee? Login(string username, string password, List<Employee> employees)
        {
            var employee = employees.Find(e => e.Username == username);
            if (employee != null && employee.Password == password)
            {
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}
