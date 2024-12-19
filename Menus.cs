using System;
using Project_1_OOP_Wojciech_Dabrowski.Employees;
using System.Collections.Generic;

namespace Project_1_OOP_Wojciech_Dabrowski
{
    public class Menus
    {
        public static void MainMenu()
        {
            EmployeeManager.SeedEmployees();
            Console.WriteLine($"Number of employees currently in the system: {EmployeeManager.GetAllEmployees().Count}");
            string directoryPath = @"C:\Users\wojt6\source\repos\Project_1_OOP_Wojciech_Dabrowski\Employees list";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = Path.Combine(directoryPath, "employees.txt");
            EmployeeManager.LoadFromFile(filePath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Saving file to: {Path.GetFullPath(filePath)}");
                Console.WriteLine("Welcome to the Hospital System");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Save and Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Login();
                }
                else if (choice == "2")
                {
                    EmployeeManager.SaveToFile(filePath);
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        private static void Login()
        {
            Console.Clear();
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var loggedInUser = Employee.Login(username, password, EmployeeManager.GetAllEmployees());

            if (loggedInUser != null)
            {
                if (loggedInUser.UserRole == Employee.Role.Administrator)
                {
                    AdminMenu();
                }
                else if (loggedInUser.UserRole == Employee.Role.Doctor)
                {
                    DoctorMenu((Doctor)loggedInUser);
                }
                else if (loggedInUser.UserRole == Employee.Role.Nurse)
                {
                    NurseMenu((Nurse)loggedInUser);
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password. Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }



        private static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Manage On-Call Schedule");
                Console.WriteLine("4. Logout");
                Console.Write("Choose an option: ");
                string adminChoice = Console.ReadLine();

                if (adminChoice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("List of Employees:");
                    foreach (var emp in EmployeeManager.GetAllEmployees())
                    {
                        Console.WriteLine($"{emp.Name} {emp.Surname} ({emp.UserRole})");
                    }
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (adminChoice == "2")
                {
                    AddNewEmployee();
                }
                else if (adminChoice == "3")
                {
                    ManageSchedule();
                }
                else if (adminChoice == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        private static void AddNewEmployee()
        {
            Console.Clear();
            Console.WriteLine("Add New Employee:");
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Surname: ");
            string surname = Console.ReadLine();

            Console.Write("Enter PESEL: ");
            int pesel = int.Parse(Console.ReadLine());

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Select Role: 1. Doctor, 2. Nurse, 3. Administrator");
            Employee.Role role = (Employee.Role)(int.Parse(Console.ReadLine()) - 1);

            Employee newEmployee;
            if (role == Employee.Role.Doctor)
            {
                Console.WriteLine("Choose Specialization: 1. Cardiologist, 2. Urologist, 3. Neurologist, 4. Laryngologist");
                var specialty = (Doctor.Specialization)(int.Parse(Console.ReadLine()) - 1);

                Console.Write("Enter PWZ (7 digits): ");
                string pwz = Console.ReadLine();

                newEmployee = new Doctor(name, surname, pesel, username, password, specialty, pwz, role);
            }
            else if (role == Employee.Role.Nurse)
            {
                newEmployee = new Nurse(name, surname, pesel, username, password, role);
            }
            else
            {
                newEmployee = new Administrator(name, surname, pesel, username, password, role);
            }

            EmployeeManager.AddEmployee(newEmployee);
            Console.WriteLine($"Employee {name} {surname} added successfully. Press any key to return to the menu...");
            Console.ReadKey();
        }

        private static void ManageSchedule()
        {
            Console.Clear();
            Console.WriteLine("Manage On-Call Schedule:");
            Console.WriteLine("1. Add On-Call Day");
            Console.WriteLine("2. Remove On-Call Day");
            Console.Write("Choose an option: ");
            string scheduleChoice = Console.ReadLine();

            if (scheduleChoice == "1")
            {
                AddOnCallDay();
            }
            else if (scheduleChoice == "2")
            {
                RemoveOnCallDay();
            }
            else
            {
                Console.WriteLine("Invalid option. Press any key to return...");
                Console.ReadKey();
            }
        }

        private static void AddOnCallDay()
        {
            Console.Clear();
            Console.WriteLine("Assign On-Call Day:");
            var employees = EmployeeManager.GetAllEmployees();

            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i] is Doctor || employees[i] is Nurse)
                {
                    Console.WriteLine($"{i + 1}. {employees[i].Name} {employees[i].Surname} ({employees[i].UserRole})");
                }
            }

            Console.Write("Choose an employee by number: ");
            if (int.TryParse(Console.ReadLine(), out int employeeIndex) &&
                employeeIndex > 0 && employeeIndex <= employees.Count)
            {
                var selectedEmployee = employees[employeeIndex - 1];
                Console.Write("Enter the on-call day (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime onCallDay))
                {
                    if (selectedEmployee is Doctor doctor)
                    {
                        doctor.AddOnCallDay(onCallDay);
                    }
                    else if (selectedEmployee is Nurse nurse)
                    {
                        nurse.AddOnCallDay(onCallDay);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private static void RemoveOnCallDay()
        {
            Console.Clear();
            Console.WriteLine("Remove On-Call Day:");
            var employees = EmployeeManager.GetAllEmployees();

            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i] is Doctor || employees[i] is Nurse)
                {
                    Console.WriteLine($"{i + 1}. {employees[i].Name} {employees[i].Surname} ({employees[i].UserRole})");
                }
            }

            Console.Write("Choose an employee by number: ");
            if (int.TryParse(Console.ReadLine(), out int employeeIndex) &&
                employeeIndex > 0 && employeeIndex <= employees.Count)
            {
                var selectedEmployee = employees[employeeIndex - 1];
                Console.Write("Enter the on-call day (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime onCallDay))
                {
                    if (selectedEmployee is Doctor doctor)
                    {
                        doctor.RemoveOnCallDay(onCallDay);
                    }
                    else if (selectedEmployee is Nurse nurse)
                    {
                        nurse.RemoveOnCallDay(onCallDay);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private static void DoctorMenu(Doctor doctor)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, Dr. {doctor.Name}!");
                Console.WriteLine("1. View All Doctors");
                Console.WriteLine("2. View Your On-Call Schedule");
                Console.WriteLine("3. Logout");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("List of Doctors:");
                    foreach (var employee in EmployeeManager.GetAllEmployees())
                    {
                        if (employee is Doctor doc)
                        {
                            Console.WriteLine($"Dr. {doc.Name} {doc.Surname} - {doc.Specialty}");
                        }
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.Write("Enter the month (1-12): ");
                    if (int.TryParse(Console.ReadLine(), out int month))
                    {
                        doctor.DisplayOnCallSchedule(month);
                    }
                    else
                    {
                        Console.WriteLine("Invalid month.");
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        private static void NurseMenu(Nurse nurse)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, Nurse {nurse.Name}!");
                Console.WriteLine("1. View All Nurses");
                Console.WriteLine("2. View Your On-Call Schedule");
                Console.WriteLine("3. Logout");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("List of Nurses:");
                    foreach (var employee in EmployeeManager.GetAllEmployees())
                    {
                        if (employee is Nurse nur)
                        {
                            Console.WriteLine($"{nur.Name} {nur.Surname}");
                        }
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.Write("Enter the month (1-12): ");
                    if (int.TryParse(Console.ReadLine(), out int month))
                    {
                        nurse.DisplayOnCallSchedule(month);
                    }
                    else
                    {
                        Console.WriteLine("Invalid month.");
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }
    }
}
