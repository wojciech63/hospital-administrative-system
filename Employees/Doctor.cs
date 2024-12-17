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

        public bool AddOnCallDay(DateTime day)
        {
            if (onCallDays.Contains(day))
            {
                Console.WriteLine("Error: This day is already assigned");
                return false;
            }

            if (onCallDays.Any(d => Math.Abs((d - day).Days) == 1))
            {
                Console.WriteLine("Error: Cannot schedule consecutive days.");
                return false;
            }

            if (onCallDays.Count(d => d.Month == day.Month) > 10)
            {
                Console.WriteLine("Error: Cannot have more than 10 days per month");
                return false;
            }

            onCallDays.Add(day);
            return true;
        }

    }
}
