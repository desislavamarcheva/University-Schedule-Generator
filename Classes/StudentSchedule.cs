using System;
using System.Collections.Generic;
using System.Text;
using UniSchedule.Helpers;

namespace UniSchedule.Classes
{
    class StudentSchedule : Schedule
    {
        public string Spec { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public string Subgroup { get; set; }

        public StudentSchedule()
        {
        }

        public StudentSchedule(Group gr)
        {
            Spec = gr.Spec;
            Course = int.Parse(gr.Course);
            Group = gr.GroupNum;
            Subgroup = gr.SubGroup;
        }
    }
}
