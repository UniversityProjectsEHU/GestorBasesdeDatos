using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class ClassDropProfile:Query
    {
        private string aTable;
        private string nombre;
        private string result;
        public ClassDropProfile(String table, String pnombre)
        {
            aTable = table;
            pnombre = nombre;
        }

        public override string getClass()
        {
            return "dropprofile";
        }

        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            string pathfileDEF = @"..//..//..//data//" + dbname + "//" + aTable + ".def";
            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + aTable + ".data";
            string pathfileSEC = @"..//..//..//data//" + dbname + "//" + aTable + ".sec";
            string pathfilePROFfichero = @"..//..//..//data//" + dbname + "//profiles//" + nombre + ".pf";  //Ponunciation like "púfff"

            File.Delete(pathfilePROFfichero);
           
            result = Constants.SecurityProfileDeleted;
        }
    }
}
}
