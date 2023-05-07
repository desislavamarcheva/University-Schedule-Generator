using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UniSchedule.Helpers
{
    class Board
    {
        public string Day { get; set; }
        public int StartsAt { get; set; }
        public int EndsAt { get; set; }

        public int isEmpty()
        {
            return String.IsNullOrEmpty(Day) && StartsAt == 0 && EndsAt == 0 ? 1 : 0;
        }
    }
}
