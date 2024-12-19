using System;
using System.Collections.Generic;

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

            if (_onCallSchedule[month].Count >= 10 || _onCallSchedule[month].Exists(d => Math.Abs((d - day).Days) == 1))
            {
                Console.WriteLine("Error: Schedule conflicts or exceeds allowed days.");
                return false;
            }

            _onCallSchedule[month].Add(day);
            Console.WriteLine($"On-call day {day:yyyy-MM-dd} added successfully for Nurse {Name}.");
            return true;
        }

        public void RemoveOnCallDay(DateTime day)
        {
            int month = day.Month;
            if (_onCallSchedule.ContainsKey(month) && _onCallSchedule[month].Remove(day))
            {
                Console.WriteLine($"On-call day {day:yyyy-MM-dd} removed successfully for Nurse {Name}.");
            }
            else
            {
                Console.WriteLine("Day not found in the schedule.");
            }
        }

        public List<DateTime> GetOnCallScheduleForMonth(int month)
        {
            return _onCallSchedule.ContainsKey(month) ? _onCallSchedule[month] : new List<DateTime>();
        }

        public void DisplayOnCallSchedule(int month)
        {
            if (_onCallSchedule.ContainsKey(month))
            {
                Console.WriteLine($"On-call schedule for Nurse {Name} in month {month}:");
                foreach (var day in _onCallSchedule[month])
                {
                    Console.WriteLine(day.ToString("yyyy-MM-dd"));
                }
            }
            else
            {
                Console.WriteLine($"No on-call schedule for Nurse {Name} in month {month}.");
            }
        }

        public string GetOnCallScheduleString()
        {
            var schedule = new List<string>();
            foreach (var month in _onCallSchedule)
            {
                foreach (var day in month.Value)
                {
                    schedule.Add($"{month.Key}-{day:yyyy-MM-dd}");
                }
            }
            return string.Join(",", schedule);
        }

        public void LoadOnCallScheduleFromString(string scheduleData)
        {
            var entries = scheduleData.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var entry in entries)
            {
                var parts = entry.Split('-');
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
