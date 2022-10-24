using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Sem_Prace_Vavra_Petr.Entities
{
    public class Linka
    {
        public ObjectId LinkaId { get; set; }
        public string Name { get; set; }
        //linka produkuje produkt z komponentů
        public ProductsEnum Product { get; set; }
        //každá linka náleží jednomu závodu
        public ObjectId ZavodId { get; set; }

        public Linka(string name, ProductsEnum product, ObjectId zavodId)
        {
            LinkaId = ObjectId.NewObjectId();
            Name = name;
            Product = product;
            ZavodId = zavodId;
        }
        public Linka(string name, ProductsEnum product)
        {
            LinkaId = ObjectId.NewObjectId();
            Name = name;
            Product = product;
        }

        [BsonCtor]
        public Linka(ObjectId _id, string name, ProductsEnum product, ObjectId _zavodId)
        {
            LinkaId = _id;
            Name = name;
            Product = product;
            ZavodId = _zavodId;
        }

    }
}
