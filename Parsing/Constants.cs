using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    class Constants
    {
        String regEXSelect = @"SELECT\s+(\*|\w+)\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+);";
        String regExDelete = @"DELETE\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+);";
        String regExInsert = @"INSERT\s+INTO\s+(\w+)\s+VALUES\s+\(([^\)]+)\);";
        String regExpUpdate = @"UPDATE\s+(\w+)\s+SET\s+([^ WHERE]+)\s+WHERE\s+(\w+>[0-9]+|\w+<[0-9]+|\w+=[0-9]+);";
        String regExTypeSelect = @"(SELECT)";
        String regExTypeInsert = @"(INSERT)";
        String regExTypeUpdate = @"(UPDATE)";
        String regExTypeDelete = @"(DELETE\s+FROM)";
        String regExTypesDropDatabase = @"(DROP\s+DATABASE)";
        String regExTypesDropTable = @"(DROP\s+TABLE)";
        String regExTypesCreateDatabase = @"(CREATE\s+DATABASE)";
        String regExTypesCreateTable = @"(CREATE\s+TABLE)";
        



    }
}
