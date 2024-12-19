using Project_1_OOP_Wojciech_Dabrowski.Employees;
using System;
using System.Collections.Generic;
using System.IO;

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

        public static void SeedEmployees()
        {
            if (_employees.Count == 0) 
            {
                _employees.Add(new Doctor("John", "Doe", 123456789, "doctor1", "password", Doctor.Specialization.Cardiologist, "1234567", Employee.Role.Doctor));
                _employees.Add(new Nurse("Jane", "Smith", 987654321, "nurse1", "password", Employee.Role.Nurse));
                _employees.Add(new Administrator("Admin", "Adminson", 111222333, "admin1", "password", Employee.Role.Administrator));
                Console.WriteLine("Initial employees seeded.");
            }
        }

        public static void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new(filePath))
                {
                    foreach (var employee in _employees)
                    {
                        if (employee is Doctor doctor)
                        {
                            writer.WriteLine($"Doctor|{doctor.Name}|{doctor.Surname}|{doctor.PESEL}|{doctor.Username}|{doctor.UserRole}|{doctor.Specialty}|{doctor.GetOnCallScheduleString()}");
                        }
                        else if (employee is Nurse nurse)
                        {
                            writer.WriteLine($"Nurse|{nurse.Name}|{nurse.Surname}|{nurse.PESEL}|{nurse.Username}|{nurse.UserRole}|{nurse.GetOnCallScheduleString()}");
                        }
                        else if (employee is Administrator admin)
                        {
                            writer.WriteLine($"Administrator|{admin.Name}|{admin.Surname}|{admin.PESEL}|{admin.Username}|{admin.UserRole}");
                        }
                    }
                }
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public static void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Data file not found. Starting fresh.");
                return;
            }

            try
            {
                _employees.Clear();

                using (StreamReader reader = new(filePath))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts[0] == "Doctor")
                        {
                            var doctor = new Doctor(
                                parts[1], parts[2], int.Parse(parts[3]), parts[4], "password", // Placeholder password
                                Enum.Parse<Doctor.Specialization>(parts[6]), parts[7], Employee.Role.Doctor);
                            doctor.LoadOnCallScheduleFromString(parts[8]);
                            _employees.Add(doctor);
                        }
                        else if (parts[0] == "Nurse")
                        {
                            var nurse = new Nurse(
                                parts[1], parts[2], int.Parse(parts[3]), parts[4], "password", Employee.Role.Nurse);
                            nurse.LoadOnCallScheduleFromString(parts[6]);
                            _employees.Add(nurse);
                        }
                        else if (parts[0] == "Administrator")
                        {
                            _employees.Add(new Administrator(
                                parts[1], parts[2], int.Parse(parts[3]), parts[4], "password", Employee.Role.Administrator));
                        }
                    }
                }
                Console.WriteLine($"Loaded {_employees.Count} employees from file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
    }
}
