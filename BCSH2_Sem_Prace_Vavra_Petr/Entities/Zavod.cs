using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Sem_Prace_Vavra_Petr.Entities
{
    public class Zavod
    {
        public ObjectId ZavodId { get; set; }
        public string Name { get; set; }
        public Adress? Adress { get; set; }

        public Zavod(string name, Adress adress) { 
            ZavodId = ObjectId.NewObjectId();
            Name = name;
            Adress = adress;
        }

        [BsonCtor]
        public Zavod(ObjectId _id, string name, Adress adress) { 
            ZavodId = _id;
            Name = name;
            Adress = adress;
        }
    }
}
