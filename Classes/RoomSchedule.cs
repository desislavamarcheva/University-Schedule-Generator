using System;
using System.Collections.Generic;
using System.Text;
using UniSchedule.Helpers;

namespace UniSchedule.Classes
{
    class RoomSchedule : Schedule
    {
        public string LabRoom { get; set; }
        public string LectureRoom { get; set; }
        public string CourseProjectRoom { get; set; }

        public RoomSchedule()
        {
        }

        public RoomSchedule(string room)
        {
            LabRoom = room;
            LectureRoom = room;
            CourseProjectRoom = room;
        }
    }
}
