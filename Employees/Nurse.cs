using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_1_OOP_Wojciech_Dabrowski.Employees
{
    public class Nurse : Employee
    {
        private readonly Dictionary<int, List<DateTime>> _onCallSchedule = new();

        public Nurse(string name, string surname, int pesel, string username, string password, Role role)
            : base(name, surname, pesel, username, password, role)
        {
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
                Console.WriteLine("Cannot assign more than 10 on-call days in a month.");
                return false;
            }

            if (_onCallSchedule[month].Any(d => Math.Abs((d - day).Days) == 1))
            {
                Console.WriteLine("Cannot assign consecutive on-call days.");
                return false;
            }

            _onCallSchedule[month].Add(day);
            Console.WriteLine($"On-call day {day:yyyy-MM-dd} added successfully.");
            return true;
        }

        public void RemoveOnCallDay(DateTime day)
        {
            int month = day.Month;

            if (_onCallSchedule.ContainsKey(month) && _onCallSchedule[month].Remove(day))
            {
                Console.WriteLine($"On-call day {day:yyyy-MM-dd} removed successfully.");
            }
            else
            {
                Console.WriteLine("On-call day not found.");
            }
        }

        public void DisplayOnCallSchedule(int month)
        {
            if (_onCallSchedule.ContainsKey(month))
            {
                Console.WriteLine($"On-call schedule for {Name} in month {month}:");
                foreach (var day in _onCallSchedule[month])
                {
                    Console.WriteLine(day.ToString("yyyy-MM-dd"));
                }
            }
            else
            {
                Console.WriteLine($"No on-call schedule for {Name} in month {month}.");
            }
        }

        public string GetOnCallScheduleString()
        {
            return string.Join(";", _onCallSchedule.SelectMany(kv => kv.Value.Select(d => $"{kv.Key}:{d:yyyy-MM-dd}")));
        }

        public void LoadOnCallScheduleFromString(string scheduleData)
        {
            if (string.IsNullOrEmpty(scheduleData)) return;

            foreach (var entry in scheduleData.Split(';'))
            {
                var parts = entry.Split(':');
                int month = int.Parse(parts[0]);
                DateTime day = DateTime.Parse(parts[1]);

                if (!_onCallSchedule.ContainsKey(month))
                {
                    _onCallSchedule[month] = new List<DateTime>();
                }
                _onCallSchedule[month].Add(day);
            }
        }
    }
}
