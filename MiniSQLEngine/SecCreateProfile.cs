using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class SecCreateProfile : Query
    {
        private string name;
        private string result;

        public SecCreateProfile(string pname)
        {
            name = pname;
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
            string pathProfiles = @"..\\..\\..\\data\\" + dbname + "\\profiles\\" + name+".pf";
            if (!File.Exists(pathProfiles))
            {
                System.IO.Directory.CreateDirectory(pathProfiles);
                result = Constants.SecurityProfileCreated;
            }
            else
            {
                result = Constants.SecurityProfileAlreadyExists;
            }
        }
    }
}
