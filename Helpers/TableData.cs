using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UniSchedule.Helpers
{
    class TableData
    {
        public string Discipline { get; set; }
        public int Course { get; set; }
        public string Spec { get; set; }
        public int LectureHorarium { get; set; }
        public string Multimedia { get; set; }
        public int SeminarGroups { get; set; }
        public int LabGroups { get; set; }
        public List<string> Rooms { get; set; }
        public List<string> CourseProjectRoom { get; set; }
    }
}
