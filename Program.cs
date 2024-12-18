using Project_1_OOP_Wojciech_Dabrowski.Employees;

class Program
{
    static void Main(string[] args)
    {
        var doctor1 = new Doctor("John", "Smith", 123456789, "johnsmith", "bestpasswordever", Doctor.Specialization.Neurologist, "1234567");
        Console.WriteLine("===== Doctor Details =====");
        Console.WriteLine($"Name: {doctor1.Name} {doctor1.Surname}");
        Console.WriteLine($"Specialization: {doctor1.Specialty}");
        Console.WriteLine($"PWZ: {doctor1.PWZ}");
        doctor1.AddOnCallDay(new DateTime(2024, 12, 1));
        doctor1.AddOnCallDay(new DateTime(2024, 12, 2));
        doctor1.DisplayOnCallSchedule(12);
        Console.WriteLine("===== Password Test =====");
        Console.WriteLine("Enter password for John Smith:");
        string inputPassword = Console.ReadLine();
        if (doctor1.VerifyPassword(inputPassword))
        {
            Console.WriteLine("Password is correct!");
        }
        else
        {
            Console.WriteLine("Password is incorrect!");
        }
    }
}