using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using System.Text;
using System.Threading.Tasks;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models.Enums;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public class Mistr
    {
        public int MistrId { get; set; }
        public string Name { get; set; }
        public ShiftsEnum Shift { get; set; }
        public int LinkaId { get; set; }

        public Mistr(string name, ShiftsEnum shift, int linkaId) {
            Name = name;
            Shift = shift;
            LinkaId = linkaId;
        }

        public Mistr(string name, ShiftsEnum shift)
        {
            Name = name;
            Shift = shift;
        }

        public override string ToString()
        {
            return "ID: " + MistrId + ", Jméno: " + Name + ", Směna: " + Shift + ", Linka: " + LinkaId;
        }

    }
}
