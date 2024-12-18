using Microsoft.VisualBasic;
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

        private readonly Dictionary<int, List<DateTime>> _onCallSchedule = new();

        public Doctor(string name, string surname, int pesel, string username, string password, Specialization specialty, string pwz) : base(name, surname, pesel, username, password)
        {
            Specialty = specialty;
            PWZ = pwz;
        }

        public bool AddOnCallDay(DateTime day)
        {

            int month = day.Month;
            
            if (!_onCallSchedule.ContainsKey(month))
            {
                _onCallSchedule[month] = new List<DateTime>();
            }

            if (_onCallSchedule[month].Count >= 10)
            {
                Console.WriteLine($"Error: Cannot assign more than 10 on-call days for {month}");
                return false;
            }

            if (_onCallSchedule[month].Any(d => Math.Abs((d - day).Days) == 1))
            {
                Console.WriteLine($"Error: Cannot assign consecutive on-call days in {month}");
                return false;
            }

            _onCallSchedule[month].Add(day);
            Console.WriteLine($"On call day {day:dd.MM.yyyy} added succesfully");
            return true;
        }

        public void DisplayOnCallSchedule(int month)
        {
            if (_onCallSchedule.ContainsKey(month))
            {
                Console.WriteLine($"On-call schedule for {Name} in month {month}");
                foreach (var day in _onCallSchedule[month])
                {
                    Console.WriteLine(day.ToShortDateString());
                }
            }
            else
            {
                Console.WriteLine($"No on-call schedule for {Name} in month {month}");
            }
        }
    }
}
