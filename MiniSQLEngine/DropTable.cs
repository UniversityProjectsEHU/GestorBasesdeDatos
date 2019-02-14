using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class DropTable : Query
    {
        String tableName;

        public DropTable(String table)
        {
            tableName = table;
        }
        public override void Run()
        {

        }
    }
}
