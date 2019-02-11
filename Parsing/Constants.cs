using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    class Constants
    {
        String regEXSelect = @"SELECT\s+(\*|\w+)\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+)";
        String regExDelete = @"DELETE\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+)";
    }
}
