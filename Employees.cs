using System.Security.Cryptography;
using System.Text;

class Employees
{
    public class Employee
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

    public class Doctor : Employee
    {
        public string Specialty { get; set; }
        public string PWZ { get; set; }

        public Doctor(string name, string surname, int pesel, string username, string password, string specialty, string pwz) : base(name, surname, pesel, username, password)
        {
            Specialty = specialty;
            PWZ = pwz;
        }
    }

    public class Nurse : Employee
    {

    }

    public class Administrator : Employee
    {

    }
}