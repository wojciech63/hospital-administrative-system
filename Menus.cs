using Project_1_OOP_Wojciech_Dabrowski.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1_OOP_Wojciech_Dabrowski
{
    public class Menus
    {
        public static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Hospital System");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
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
                        else
                        {
                            NonAdminMenu(loggedInUser);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid credentials. Please try again.");
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey();
                    }
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Exiting the system. Goodbye!");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
        
        
        
        
        
        
        static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Logout");
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
                    Console.Clear();
                    Console.WriteLine("Add new Employee:");
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
                    Console.Clear();
                    Console.WriteLine($"Employee {name} {surname} added successfully.");
                }
                else if (adminChoice == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }

        static void NonAdminMenu(Employee user)
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {user.Name}!");
            Console.WriteLine("Feature not implemented yet. You can add functionality here.");
            Console.WriteLine("\nPress any key to log out...");
            Console.ReadKey();
        }
    }
}
