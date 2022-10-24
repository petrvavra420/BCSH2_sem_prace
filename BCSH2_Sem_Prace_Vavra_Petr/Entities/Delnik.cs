using BCSH2_Sem_Prace_Vavra_Petr.Entities.Enums;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Sem_Prace_Vavra_Petr.Entities
{
    public class Delnik
    {
        public ObjectId DelnikId { get; set; }
        public string Name { get; set; }
        //každý dělník má směnu dle ShiftsEnum
        public ShiftsEnum Shift { get; set; }
        public  ObjectId StrojId { get; set; }
        public ObjectId MistrId { get; set; }

        public Delnik(string name, ShiftsEnum shift, ObjectId strojId, ObjectId mistrId) {
            DelnikId = ObjectId.NewObjectId();
            Name = name;
            Shift = shift;
            StrojId = strojId;
            MistrId = mistrId;
        }
        public Delnik(string name, ShiftsEnum shift)
        {
            DelnikId = ObjectId.NewObjectId();
            Name = name;
            Shift = shift;
        }
    }
}
