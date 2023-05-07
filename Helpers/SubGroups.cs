using System;
using System.Collections.Generic;
using System.Text;

namespace UniSchedule.Helpers
{
    class SubGroups
    {
        public Dictionary<string, int> SubGrs { get; set; }

        public SubGroups()
        {
            SubGrs = new Dictionary<string, int>();
            SubGrs.Add("еднаподгрупа", 1);
            SubGrs.Add("Еднаподгрупа", 1);
            SubGrs.Add("двеподгрупи", 2);
            SubGrs.Add("Двеподгрупи", 2);
            SubGrs.Add("триподгрупи", 3);
            SubGrs.Add("Триподгрупи", 3);
            SubGrs.Add("Четириподгрупи", 4);
            SubGrs.Add("четириподгрупи", 4);
            SubGrs.Add("Петподгрупи", 5);
            SubGrs.Add("петподгрупи", 5);
            SubGrs.Add("Шестподгрупи", 6);
            SubGrs.Add("шестподгрупи", 6);
            SubGrs.Add("седемподгрупи", 7);
            SubGrs.Add("Седемподгрупи", 7);
            SubGrs.Add("осемподгрупи", 8);
            SubGrs.Add("Осемподгрупи", 8);
            SubGrs.Add("Деветподгрупи", 9);
            SubGrs.Add("деветподгрупи", 9);
            SubGrs.Add("десетподгрупи", 10);
            SubGrs.Add("Десетподгрупи", 10);
        }
    }
}
