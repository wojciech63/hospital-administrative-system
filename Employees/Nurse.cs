using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1_OOP_Wojciech_Dabrowski.Employees
{
    public class Nurse : Employee
    {
        public Nurse(string name, string surname, int pesel, string username, string password, Role role) : base(name, surname, pesel, username, password, role)
        {

        }

        private readonly Dictionary<int, List<DateTime>> _onCallSchedule = new();

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
