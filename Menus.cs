using Project_1_OOP_Wojciech_Dabrowski.Employees;
using System;

namespace Project_1_OOP_Wojciech_Dabrowski
{
    public class Menus
    {
        public static void MainMenu()
        {
            EmployeeManager.SeedEmployees();

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

                    Console.Write("\nEnter password: ");
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
                        Console.Clear();
                        Console.WriteLine("Invalid username or password. Please try again.");
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
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
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
                            Console.WriteLine($"Doctor: {doc.Name} {doc.Surname} - Specialization: {doc.Specialty}");
                        }
                    }
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Enter the month number (1-12) to view your schedule:");
                    if (int.TryParse(Console.ReadLine(), out int month) && month >= 1 && month <= 12)
                    {
                        doctor.DisplayOnCallSchedule(month);
                    }
                    else
                    {
                        Console.WriteLine("Invalid month. Please try again.");
                    }
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (choice == "3")
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

        private static void NurseMenu(Nurse nurse)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {nurse.Name}!");
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
                        if (employee is Nurse nurseEmp)
                        {
                            Console.WriteLine($"Nurse: {nurseEmp.Name} {nurseEmp.Surname}");
                        }
                    }
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Enter the month number (1-12) to view your schedule:");
                    if (int.TryParse(Console.ReadLine(), out int month) && month >= 1 && month <= 12)
                    {
                        nurse.DisplayOnCallSchedule(month);
                    }
                    else
                    {
                        Console.WriteLine("Invalid month. Please try again.");
                    }
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
                else if (choice == "3")
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

        private static void ManageSchedule()
        {
            Console.Clear();
            Console.WriteLine("Manage On-Call Schedule:");
            Console.WriteLine("Select an employee to assign a schedule:");
            var employees = EmployeeManager.GetAllEmployees();

            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i] is Doctor || employees[i] is Nurse) // Only show Doctors and Nurses
                {
                    Console.WriteLine($"{i + 1}. {employees[i].Name} {employees[i].Surname} ({employees[i].UserRole})");
                }
            }

            Console.Write("Choose an employee by number: ");
            if (int.TryParse(Console.ReadLine(), out int employeeIndex) &&
                employeeIndex > 0 && employeeIndex <= employees.Count &&
                (employees[employeeIndex - 1] is Doctor || employees[employeeIndex - 1] is Nurse))
            {
                var selectedEmployee = employees[employeeIndex - 1];
                Console.Write("Enter the on-call day (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime onCallDay))
                {
                    if (selectedEmployee is Doctor doctor)
                    {
                        if (doctor.AddOnCallDay(onCallDay))
                        {
                            Console.WriteLine($"On-call day {onCallDay:yyyy-MM-dd} assigned to Dr. {doctor.Name}.");
                        }
                    }
                    else if (selectedEmployee is Nurse nurse)
                    {
                        if (nurse.AddOnCallDay(onCallDay))
                        {
                            Console.WriteLine($"On-call day {onCallDay:yyyy-MM-dd} assigned to Nurse {nurse.Name}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
