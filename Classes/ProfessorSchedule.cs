using System;
using System.Collections.Generic;
using System.Text;
using UniSchedule.Helpers;

namespace UniSchedule.Classes
{
    class ProfessorSchedule : Schedule
    {
        public Dictionary<string, Dictionary<int, bool>> DayFromToBusy { get; set; }
        public string Professor { get; set; }
        public string Depart { get; set; }

        public ProfessorSchedule()
        {
            DayFromToBusy = new Dictionary<string, Dictionary<int, bool>>();
        }
    }
}
