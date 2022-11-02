using BCSH2_Avalonia_Vavra_Petr_Sem.Interfaces;
using BCSH2_Avalonia_Vavra_Petr_Sem.Models;
using BCSH2_Avalonia_Vavra_Petr_Sem.ViewModels;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public class Zavod : ICollectionModels
    {
        public int ZavodId { get; set; }
        public string Name { get; set; }
        public Adress? Adress { get; set; }

        public Zavod(string name, Adress adress)
        {
            Name = name;
            Adress = adress;
        }

        public Zavod()
        {
        }

        [BsonCtor]
        public Zavod(int _id, string name, Adress adress)
        {
            ZavodId = _id;
            Name = name;
            Adress = adress;
        }

        public override string ToString()
        {
            return "ID: " + ZavodId.ToString() + ", Název: " + Name + ", Adresa: " + Adress.ToString();
        }
    }
}
