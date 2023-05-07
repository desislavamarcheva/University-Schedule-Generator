using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace UniSchedule.Helpers
{
    class PdfData : IEquatable<PdfData>, IComparable<PdfData>
    {
        public string StudyType { get; set; }
        public string Professor { get; set; }
        public string Depart { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        public List<TableData> TableDatas { get; set; }
        public Board RectBoard { get; set; }
        public Board AcademBoard { get; set; }
        public Board FacBoard { get; set; }
        public Board CatBoard { get; set; }
        public Board ContBoard { get; set; }
        public Board OtherBoard { get; set; }

        public int GetBusyIndex()
        {
            return new List<int>() {
                RectBoard.isEmpty(),
                AcademBoard.isEmpty(),
                FacBoard.isEmpty(),
                CatBoard.isEmpty(),
                ContBoard.isEmpty(),
                OtherBoard.isEmpty()
            }.Sum();
        }

        public int CompareTo([AllowNull] PdfData other)
        {
            if (GetBusyIndex() > other.GetBusyIndex())
                return -1;
            else if (GetBusyIndex() < other.GetBusyIndex())
                return 1;
            return 0;
        }

        public bool Equals([AllowNull] PdfData other)
        {
            return StudyType.Equals(other.StudyType) &&
                Professor.Equals(other.Professor) &&
                Depart.Equals(other.Depart) &&
                Semester.Equals(other.Semester) &&
                Year.Equals(other.Year) &&
                TableDatas.Equals(other.TableDatas) &&
                RectBoard.Equals(other.RectBoard) &&
                AcademBoard.Equals(other.AcademBoard) &&
                FacBoard.Equals(other.FacBoard) &&
                CatBoard.Equals(other.CatBoard) &&
                ContBoard.Equals(other.ContBoard) &&
                OtherBoard.Equals(other.OtherBoard);
        }
    }
}
