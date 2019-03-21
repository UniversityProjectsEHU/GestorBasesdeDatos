using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class Constants
    {
        public const string regExSelect = @"SELECT\s+([^ WHERE]+)\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+);";
        public const string regExDelete = @"DELETE\s+FROM\s+(\w+)\s+WHERE\s+(\w+<[0-9]+|\w+>[0-9]+|\w+=[0-9]+);";
        public const string regExInsert = @"INSERT\s+INTO\s+(\w+)\s+VALUES \(([^\)]+)\);";
        public const string regExInsert2 = @"INSERT\s+INTO\s+([^\(]+)\(([^\)]+)\)\s+VALUES \(([^\)]+)\);";
        public const string regExpUpdate = @"UPDATE\s+(\w+)\s+SET\s+([^WHERE]+)\s+WHERE\s+(\w+>\w+|\w+<\w+|\w+=\w+);";
        public const string regExpCreateDatabase = @"CREATE DATABASE\s+(\w+);";
        public const string regExpDropDatabase = @"DROP DATABASE\s+(\w+);";
        public const string regExpDropTable = @"DROP\s+TABLE\s+(\w+);";
        public const string regExpCreateTable = @"CREATE TABLE\s+(\w+)\s+\(([^\)]+)\);";
        public const string regExTypeSelect = @"(SELECT)";
        public const string regExTypeInsert = @"(INSERT)";
        public const string regExTypeUpdate = @"(UPDATE)";
        public const string regExTypeDelete = @"(DELETE\s+FROM)";
        public const string regExTypesDropDatabase = @"(DROP\s+DATABASE)";
        public const string regExTypesDropTable = @"(DROP\s+TABLE)";
        public const string regExTypesCreateDatabase = @"(CREATE\s+DATABASE)";
        public const string regExTypesCreateTable = @"(CREATE\s+TABLE)";
        public const string regExConditionAttribute = @"(\w+)>\w+|(\w+)<\w+|(\w+)=\w+";
        public const string regExConditionValue = @"\w+(>\w+)|\w+(<\w+)|\w+(=\w+)";
        public const string CreateDatabaseSuccess = "Database created";
        public const string DeleteDatabaseSuccess = "Database deleted";
        public const string BackupDatabaseSuccess = "Database backed up";

        public const string CreateTableSuccess = "Table created";
        public const string DropTableSuccess = "Table dropped";
        public const string InsertSuccess = "Tuple added";
        public const string TupleDeleteSuccess = "Tuple(s) deleted";
        public const string TupleUpdateSuccess = "Tuple(s) updated";

        public const string Error = "ERROR: ";

        public const string WrongSyntax = Error + "Syntactical error";
        public const string DatabaseDoesNotExist = Error + "Database does not exist";
        public const string TableDoesNotExist = Error + "Table does not exist";
        public const string ColumnDoesNotExist = Error + "Column does not exist";
        public const string IncorrectDataType = Error + "Incorrect data type";
        public const string TableAlreadyExists = Error + "Table already exists";
    }
}
