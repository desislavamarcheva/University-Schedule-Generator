using System;
using System.Collections.Generic;
using System.Text;

namespace UniSchedule.Helpers
{
    class SeminarGroups
    {
        public Dictionary<string, int> SeminarGrs { get; set; }

        public SeminarGroups()
        {
            SeminarGrs = new Dictionary<string, int>();
            SeminarGrs.Add("еднагрупа", 1);
            SeminarGrs.Add("Еднагрупа", 1);
            SeminarGrs.Add("двегрупи", 2);
            SeminarGrs.Add("Двегрупи", 2);
            SeminarGrs.Add("тригрупи", 3);
            SeminarGrs.Add("Тригрупи", 3);
            SeminarGrs.Add("Четиригрупи", 4);
            SeminarGrs.Add("четиригрупи", 4);
            SeminarGrs.Add("Петгрупи", 5);
            SeminarGrs.Add("петгрупи", 5);
            SeminarGrs.Add("Шестгрупи", 6);
            SeminarGrs.Add("шестгрупи", 6);
            SeminarGrs.Add("седемгрупи", 7);
            SeminarGrs.Add("Седемгрупи", 7);
            SeminarGrs.Add("осемгрупи", 8);
            SeminarGrs.Add("Осемгрупи", 8);
            SeminarGrs.Add("Деветгрупи", 9);
            SeminarGrs.Add("деветгрупи", 9);
            SeminarGrs.Add("десетгрупи", 10);
            SeminarGrs.Add("Десетгрупи", 10);
        }
    }
}
