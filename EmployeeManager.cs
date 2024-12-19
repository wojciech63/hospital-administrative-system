using Project_1_OOP_Wojciech_Dabrowski.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1_OOP_Wojciech_Dabrowski
{
    public static class EmployeeManager
    {
        private static List<Employee> _employees = new();

        public static void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }
        public static List<Employee> GetAllEmployees()
        {
            return _employees;
        }
        public static Employee? FindEmployeeByUsername(string username)
        {
            return _employees.Find(e => e.Username == username);
        }

        public static void SeedEmployees()
        {
            _employees.Add(new Doctor("John", "Doe", 123456789, "doctor1", "securepassword", Doctor.Specialization.Cardiologist, "1234567", Employee.Role.Doctor));
            _employees.Add(new Administrator("Mike", "Tyson", 987654321, "admin1", "passwordsecure", Employee.Role.Administrator));
        }

    }
}
