using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    class Constants
    {
        public const String regExSelect = @"SELECT\s+(\*|\w+)\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+);";
        public const String regExDelete = @"DELETE\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+);";
        public const String regExInsert = @"INSERT\s+INTO\s+(\w+)\s+VALUES\s+\(([^\)]+)\);";
        public const String regExpUpdate = @"UPDATE\s+(\w+)\s+SET\s+([^ WHERE]+)\s+WHERE\s+(\w+>[0-9]+|\w+<[0-9]+|\w+=[0-9]+);";
        public const String regExpCreateDatabase = @"CREATE DATABASE\s+(\w+);";
        public const String regExpDropDatabase = @"DROP DATABASE\s+(\w+);";
        public const String regExpDropTable = @"DROP TABLE(\w+);";
        public const String regExpCreateTable = @"CREATE TABLE (\w+)\(([^\)]+)\);";
        public const String regExTypeSelect = @"(SELECT)";
        public const String regExTypeInsert = @"(INSERT)";
        public const String regExTypeUpdate = @"(UPDATE)";
        public const String regExTypeDelete = @"(DELETE\s+FROM)";
        public const String regExTypesDropDatabase = @"(DROP\s+DATABASE)";
        public const String regExTypesDropTable = @"(DROP\s+TABLE)";
        public const String regExTypesCreateDatabase = @"(CREATE\s+DATABASE)";
        public const String regExTypesCreateTable = @"(CREATE\s+TABLE)";
        



    }
}
