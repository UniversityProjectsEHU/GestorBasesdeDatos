using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class ClassCreateProfile : Query
    {
        private string aTable;
        private string nombre;
        private string result;
        public ClassCreateProfile(String table, String pnombre)
        {
            aTable = table;
            pnombre = nombre;
        }

        public override string getClass()
        {
            return "createprofile";
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
            string pathfilePROFfichero = @"..//..//..//data//" + dbname + "//profiles//" + nombre  + ".pf";  //Ponunciation like "púfff"

            using (FileStream stream2 = File.Create(pathfilePROFfichero))
            {
            }
            result = Constants.SecurityProfileCreated;
        }
    }
}
