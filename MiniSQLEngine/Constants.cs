using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class Constants
    {
        string regExp = @"UPDATE\s+(\w+)\s+SET\s+([^ WHERE]+)\s+WHERE\s+(\w+>[0-9]+|\w+<[0-9]+|\w+=[0-9]+);";
    }
}
