using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassCreateDatabase : Query
    {
        private string tableName; 
        public ClassCreateDatabase(string pName)
        {
            pName = tableName;
        }
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
