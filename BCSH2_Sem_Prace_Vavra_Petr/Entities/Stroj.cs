using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Sem_Prace_Vavra_Petr.Entities
{
    public class Stroj
    {
        public ObjectId StrojId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Rychlost výroby stroje v sekundách.
        /// </summary>
        public int ManufacturingSpeed { get; set; }
        public ObjectId LinkaId { get; set; }

        public Stroj(string name, int manufacturingSpeed, ObjectId linkaId)
        {
            StrojId = ObjectId.NewObjectId();
            Name = name;
            ManufacturingSpeed = manufacturingSpeed;
            LinkaId = linkaId;
        }
        public Stroj(string name, int manufacturingSpeed)
        {
            StrojId = ObjectId.NewObjectId();
            Name = name;
            ManufacturingSpeed = manufacturingSpeed;
        }

        [BsonCtor]
        public Stroj(ObjectId _id, string name, int manufacturingSpeed, ObjectId _linkaId)
        {
            StrojId = _id;
            Name = name;
            ManufacturingSpeed = manufacturingSpeed;
            LinkaId = _linkaId;
        }
    }
}
