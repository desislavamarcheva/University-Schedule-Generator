using System;
using System.Collections.Generic;
using System.Text;

namespace UniSchedule.Helpers
{
    class LectureRooms
    {
        public List<string> AllLectureRooms { get; set; }

        public LectureRooms()
        {
            AllLectureRooms = new List<string> { "114M", "212E" };
        }
    }
}
