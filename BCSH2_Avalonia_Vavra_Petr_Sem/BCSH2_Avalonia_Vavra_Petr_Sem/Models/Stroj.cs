using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public class Stroj : ICollectionModels
    {
        public int StrojId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Rychlost výroby stroje v sekundách.
        /// </summary>
        public int ManufacturingSpeed { get; set; }
        public int LinkaId { get; set; }

        public Stroj(string name, int manufacturingSpeed,int linkaId)
        {
            Name = name;
            ManufacturingSpeed = manufacturingSpeed;
            LinkaId = linkaId;
        }
        public Stroj(string name, int manufacturingSpeed)
        {
            Name = name;
            ManufacturingSpeed = manufacturingSpeed;
        }

        [BsonCtor]
        public Stroj(int _id, string name, int manufacturingSpeed, int _linkaId)
        {
            StrojId = _id;
            Name = name;
            ManufacturingSpeed = manufacturingSpeed;
            LinkaId = _linkaId;
        }

        public override string ToString()
        {
            return "ID: " + StrojId + ", Název: " + Name + ", Rychlost výroby: " + ManufacturingSpeed + "s " + ",Linka" + LinkaId; 
        }
    }
}
