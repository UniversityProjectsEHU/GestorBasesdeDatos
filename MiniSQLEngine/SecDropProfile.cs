using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class SecDropProfile : Query
    {
        private string name;
        private string result;

        public SecDropProfile(string pname)
        {
            name = pname;
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
            string pathProfiles = @"..\\..\\..\\data\\" + dbname + "\\profiles\\" + name+".pf";
            if (!File.Exists(pathProfiles))
            {
                result = Constants.SecurityProfileDoesNotExist;
            }
            else 
            {

                System.IO.File.Delete(pathProfiles);
                result = Constants.SecurityProfileDeleted;
            }
        }
    }
}
