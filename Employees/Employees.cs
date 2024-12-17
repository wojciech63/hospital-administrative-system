using System.Security.Cryptography;
using System.Text;

class Employees
{
    public enum Specialization
    {
        Cardiologist,
        Urologist,
        Neurologist,
        Laryngologist
    }



    public abstract class Employee
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
        public Specialization Specialty { get; set; }
        private string _pwz;
        public string PWZ
        {
            get { return _pwz; }
            set
            {
                if (value.Length == 7 && value.All(char.IsDigit))
                {
                    _pwz = value;
                }
                else
                {
                    throw new ArgumentException("PWZ number must be 7 digits long and contain numbers only");
                }
            }
        }

        private List<DateTime> onCallDays = new List<DateTime>();

        public Doctor(string name, string surname, int pesel, string username, string password, Specialization specialty, string pwz) : base(name, surname, pesel, username, password)
        {
            Specialty = specialty;
            PWZ = pwz;
        }

    }
    
    /*public class Nurse : Employee
    {

    }

    public class Administrator : Employee
    {

    }
    */
   
}