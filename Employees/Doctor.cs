using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Employees;

namespace Project_1_OOP_Wojciech_Dabrowski.Employees
{
    public class Doctor : Employee
    {
        public enum Specialization
        {
            Cardiologist,
            Urologist,
            Neurologist,
            Laryngologist
        }
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
}
