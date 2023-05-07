using System;
using System.Collections.Generic;
using System.Text;

namespace UniSchedule.Classes
{
    public class Lecture
    {
        public string Professor { get; set; }
        public string Spec { get; set; }
        public int Course { get; set; }
        public string Discipline { get; set; }
        public int LectureHorarium { get; set; }
        public int SeminarGroup { get; set; }
        public string Group { get; set; }
        public string Subgroup { get; set; } 
        public List<string> LabRooms { get; set; }
        public List<string> CourseProjectRoom { get; set; }


        public Lecture()
        {
            LabRooms = new List<string>();
            CourseProjectRoom = new List<string>();
        }

    }
}
