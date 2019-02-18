
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassDropTable : Query
    {
        String tableName;

        public ClassDropTable(String table)
        {
            tableName = table;
        }
        public string GetName()
        {
            return tableName;
        }
        public override void Run()
        {

        }
    }
}
