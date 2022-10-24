using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using System.Text;
using System.Threading.Tasks;
using BCSH2_Sem_Prace_Vavra_Petr.Entities.Enums;

namespace BCSH2_Sem_Prace_Vavra_Petr.Entities
{
    public class Mistr
    {
        public ObjectId MistrId { get; set; }
        public string Name { get; set; }
        public ShiftsEnum Shift { get; set; }
        public ObjectId LinkaId { get; set; }

        public Mistr(string name, ShiftsEnum shift, ObjectId linkaId) {
            MistrId = ObjectId.NewObjectId();
            Name = name;
            Shift = shift;
            LinkaId = linkaId;
        }

        public Mistr(string name, ShiftsEnum shift)
        {
            MistrId = ObjectId.NewObjectId();
            Name = name;
            Shift = shift;
        }

    }
}
