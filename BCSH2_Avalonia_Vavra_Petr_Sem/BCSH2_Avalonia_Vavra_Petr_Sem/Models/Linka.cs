using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public class Linka
    {
        public int LinkaId { get; set; }
        public string Name { get; set; }
        //linka produkuje produkt z komponentů
        public ProductsEnum Product { get; set; }
        //každá linka náleží jednomu závodu
        public int ZavodId { get; set; }

        public Linka(string name, ProductsEnum product, int zavodId)
        {
            Name = name;
            Product = product;
            ZavodId = zavodId;
        }
        public Linka(string name, ProductsEnum product)
        {
            Name = name;
            Product = product;
        }

        [BsonCtor]
        public Linka(int _id, string name, ProductsEnum product, int _zavodId)
        {
            LinkaId = _id;
            Name = name;
            Product = product;
            ZavodId = _zavodId;
        }

        public override string ToString()
        {
            return "ID: " + LinkaId + ", Název: " + Name + ", Produkt: " + Product + ", Závod: " + ZavodId;
        }

    }
}
