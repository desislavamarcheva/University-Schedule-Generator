using System;
using System.Collections.Generic;
using System.Text;

namespace UniSchedule.Helpers
{
    class Group
    {
        public string Spec { get; set; }
        public string Course { get; set; }
        public string GroupNum { get; set; } = "_";
        public string SubGroup { get; set; } = "_";

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Spec.GetHashCode();
            hash = (hash * 7) + Course.GetHashCode();
            hash = (hash * 7) + GroupNum.GetHashCode();
            hash = (hash * 7) + SubGroup.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            return Spec.Equals(((Group)obj).Spec) && Course.Equals(((Group)obj).Course) && GroupNum.Equals(((Group)obj).GroupNum) && SubGroup.Equals(((Group)obj).SubGroup);
        }
    }
}
