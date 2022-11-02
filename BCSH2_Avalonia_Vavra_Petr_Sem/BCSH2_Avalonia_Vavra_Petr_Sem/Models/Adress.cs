using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public class Adress
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int PostalCode { get; set; }

        public Adress(string Country, string City, string Street, int houseNumber, int PostalCode)
        {
            this.Country = Country;
            this.City = City;
            this.Street = Street;
            this.HouseNumber = houseNumber;
            this.PostalCode = PostalCode;
        }

        public override string ToString()
        {
            return Country + " " + City + " " + Street + " " + PostalCode;
        }

    }
}
