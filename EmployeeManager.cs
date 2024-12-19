using Project_1_OOP_Wojciech_Dabrowski.Employees;
using System;
using System.Collections.Generic;

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
            _employees.Add(new Doctor("John", "Doe", 123456789, "doctor1", "securepassword", Doctor.Specialization.Cardiologist, "1234567", Employee.Role.Doctor));
            _employees.Add(new Nurse("Jane", "Smith", 987654321, "nurse1", "password", Employee.Role.Nurse));
            _employees.Add(new Administrator("Mike", "Tyson", 111222333, "admin1", "adminpass", Employee.Role.Administrator));
            Console.WriteLine($"Seeded {_employees.Count} employees."); // Debug output
        }


        public static void SaveToFile(string filePath)
        {
            Console.WriteLine($"Number of employees to save: {_employees.Count}");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var employee in _employees)
                {
                    if (employee is Doctor doctor)
                    {
                        string line = $"Doctor|{doctor.Name}|{doctor.Surname}|{doctor.PESEL}|{doctor.Username}|{doctor.UserRole}|{doctor.Specialty}|{doctor.GetOnCallScheduleString()}";
                        writer.WriteLine(line);
                        Console.WriteLine($"Writing: {line}"); 
                    }
                    else if (employee is Nurse nurse)
                    {
                        string line = $"Nurse|{nurse.Name}|{nurse.Surname}|{nurse.PESEL}|{nurse.Username}|{nurse.UserRole}|{nurse.GetOnCallScheduleString()}";
                        writer.WriteLine(line);
                        Console.WriteLine($"Writing: {line}");
                    }
                    else if (employee is Administrator admin)
                    {
                        string line = $"Administrator|{admin.Name}|{admin.Surname}|{admin.PESEL}|{admin.Username}|{admin.UserRole}";
                        writer.WriteLine(line);
                        Console.WriteLine($"Writing: {line}"); 
                    }
                }
            }
            Console.WriteLine($"Data saved successfully to {filePath}.");
        }


        public static void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No data file found. Starting with an empty employee list.");
                return;
            }

            _employees.Clear();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"Reading line: {line}"); // Debug output
                    var parts = line.Split('|');
                    if (parts[0] == "Doctor")
                    {
                        var doctor = new Doctor(
                            parts[1], parts[2], int.Parse(parts[3]), parts[4], "password", // Replace "password" as needed
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
                        _employees.Add(new Administrator(parts[1], parts[2], int.Parse(parts[3]), parts[4], "password", Employee.Role.Administrator));
                    }
                }
            }
            Console.WriteLine($"Loaded {_employees.Count} employees from file."); // Debug output
        }



    }
}
