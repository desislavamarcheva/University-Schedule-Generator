using System;
using System.Collections.Generic;
using System.Text;

namespace UniSchedule.Classes
{
    public class Schedule
    {
        public Dictionary<string, Dictionary<int, Lecture>> Timetable { get; set; }

        public Schedule()
        {
            Timetable = new Dictionary<string, Dictionary<int, Lecture>>();
        }

        public bool IsFree(string day, int hour)
        {
            if (Timetable.ContainsKey(day) && Timetable[day].ContainsKey(hour))
                return false;
            return true;
        }

        public void Add(string day, int hour, Lecture lecture)
        {
            if (!Timetable.ContainsKey(day))
                Timetable.Add(day, new Dictionary<int, Lecture>());
            Timetable[day].Add(hour, lecture);
        }
    }
}
