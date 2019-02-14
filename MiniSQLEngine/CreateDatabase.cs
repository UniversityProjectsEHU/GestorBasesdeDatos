using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class CreateDatabase : Query
    {
        private string tableName; 
        public CreateDatabase(string pName)
        {
            pName = tableName;
        }
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
