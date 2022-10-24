using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Sem_Prace_Vavra_Petr
{
    public class DatabaseTools
    {
        /// <summary>
        /// Vrátí instanci LiteDB podle parametru databasePath.
        /// Pokud databázový soubor neexistuje bude vytvořen.
        /// </summary>
        /// <param name="databasePath">test</param>
        /// <returns>Vrací instanci LiteDatabase dle cesty</returns>
        public LiteDatabase connectToDatabase(string databasePath)
        {

            return new LiteDatabase(databasePath);
        }



    }
}
