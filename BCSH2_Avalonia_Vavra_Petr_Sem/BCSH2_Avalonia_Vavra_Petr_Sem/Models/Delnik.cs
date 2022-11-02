using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models.Enums;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public class Delnik : ICollectionModels
    {
        public int DelnikId { get; set; }
        public string Name { get; set; }
        //každý dělník má směnu dle ShiftsEnum
        public ShiftsEnum Shift { get; set; }
        public  int StrojId { get; set; }
        public int MistrId { get; set; }

        public Delnik(string name, ShiftsEnum shift, int strojId, int mistrId) {
            Name = name;
            Shift = shift;
            StrojId = strojId;
            MistrId = mistrId;
        }
        public Delnik(string name, ShiftsEnum shift)
        {
            Name = name;
            Shift = shift;
        }

        public override string ToString()
        {
            return "ID: " + DelnikId + ", Jméno: " + Name + ", Směna: " + Shift + ", Stroj: " + StrojId + ", Mistr: " + MistrId; 
        }
    }
}
