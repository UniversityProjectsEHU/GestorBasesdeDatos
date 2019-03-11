
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassDropTable : Query
    {
        String tableName;
        String result;

        public ClassDropTable(String table)
        {
            tableName = table;
        }

        public override string getClass()
        {
            return "droptable";
        }

        public string GetName()
        {
            return tableName;
        }

        public override void Run(string dbname)
        {
            string pathfileDEF = @"..//..//..//data//" + dbname + "//" + tableName + ".def";
            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + tableName + ".data";

            if (!File.Exists(pathfileDATA) || !File.Exists(pathfileDEF))
            {
                result = Constants.TableDoesNotExist;
            }

            else
            {
                System.IO.File.Delete(pathfileDEF);
                System.IO.File.Delete(pathfileDATA);
                result = "ok";
            }
        }
        public string GetResult()
        {
            return result;
        }
    }
}
