using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class Engine
    {
        public bool Query(string queryString, out string result)
        {
            result = "resultado";
            return true;
        }
       
    }
    public abstract class Query
    {
        public Query()
        {

        }
        public abstract void Run();
    }
}
