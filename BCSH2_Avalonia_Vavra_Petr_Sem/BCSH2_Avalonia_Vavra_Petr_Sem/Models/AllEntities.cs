using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Avalonia_Vavra_Petr_Sem.Models
{
    public static class AllEntities
    {
        public static List<String> entities = new List<string> {
            "Adresy",
            "Dělníci",
            "Linky",
            "Mistři",
            "Stroje",
            "Závody"
        };
        public static readonly string Adresy = "Adresy";
        public static readonly string Delnici = "Dělníci";
        public static readonly string Linky = "Linky";
        public static readonly string Mistri = "Mistři";
        public static readonly string Stroje = "Stroje";
        public static readonly string Zavody = "Zavody";
    }
}
