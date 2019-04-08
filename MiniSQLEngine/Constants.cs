using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class Constants
    {
        public const string regExSelect2 = @"SELECT\s+([\w*]+|(?:\w+,\w+)+)\s+FROM\s+(\w+);";
        public const string regExSelect = @"SELECT\s+([\w*]+|(?:\w+,\w+)+)\s+FROM\s+(\w+)\s+WHERE\s+(.+<.+|.+>.+|.+=.+);";
        public const string regExDelete = @"DELETE\s+FROM\s+(\w+)\s+WHERE\s+(.+<.+|.+>.+|.+=.+);";
        public const string regExInsert = @"INSERT\s+INTO\s+(\w+)\s+VALUES\s*\(([^\)]+)\);";
        public const string regExInsert2 = @"INSERT\s+INTO\s+([^\(]+)\(([^\)]+)\)\s+VALUES\s*\(([^\)]+)\);";
        public const string regExpUpdate = @"UPDATE\s+(\w+)\s+SET\s+([^WHERE]+)\s+WHERE\s+(.+>.+|.+<.+|.+=.+);";
        public const string regExpCreateDatabase = @"CREATE DATABASE\s+(\w+);";
        public const string regExpDropDatabase = @"DROP DATABASE\s+(\w+);";
        public const string regExpDropTable = @"DROP\s+TABLE\s+(\w+);";
        public const string regExpCreateTable = @"CREATE TABLE\s+(\w+)\s*\(([^\)]+)\);";
        public const string regExTypeSelect = @"(SELECT)\s+([\w*]+|(?:\w+,\w+)+)\s+FROM";
        public const string regExTypeInsert = @"(INSERT)\s+INTO";
        public const string regExTypeUpdate = @"(UPDATE)\s+\w+\s+SET";
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

        //SECURITY
        public const string regSecCreateProfile = @"CREATE\sSECURITY\sPROFILE\s(\w+);";
        public const string regSecDropProfile = @"DROP\sSECURITY\sPROFILE\s(\w+);";
        public const string regSecGrant = @"GRANT\s(DELETE||UPDATE||INSERT||SELECT)\sON\s(\w+)\sTO\s(\w+);";
        public const string regSecRevoke = @"REVOKE\s(DELETE||UPDATE||INSERT||SELECT)\sON\s(\w+)\sTO\s(\w+);";
        public const string regSecAddUser = @"ADD\s+USER\s+\((\w+),\s+(\w+),\s+(\w+)\);";
        public const string regSecDeleteUser = @"DELETE\sUSER\s(\w+);";

        public const string SecurityProfileCreated = "Security profile created";
        public const string SecurityUserCreated = "Security user created";
        public const string SecurityProfileDeleted = "Security profile deleted";
        public const string SecurityUserDeleted = "Security user deleted";
        public const string SecurityPrivilegeGranted = "Security privilege granted";
        public const string SecurityPrivilegeRevoked = "Security privilege revoked";

        public const string SecurityNotSufficientPrivileges = Error + "Not sufficient privileges";
        public const string SecurityProfileAlreadyExists = Error + "Security profile already exists";
        public const string SecurityUserAlreadyExists = Error + "Security user already exists";
        public const string SecurityProfileDoesNotExist = Error + "Security profile does not exist";
        public const string SecurityUserDoesNotExist = Error + "Security user does not exist";

        public const string regExTypeSecCreateProfile = @"(CREATE SECURITY PROFILE)";
        public const string regExTypeSecDropProfile = @"(DROP SECURITY PROFILE)";
        public const string regExTypeSecGrant = @"(GRANT)";
        public const string regExTypeSecRevoke = @"(REVOKE)";
        public const string regExTypeSecAddUser = @"(ADD\s+USER)";
        public const string regExTypeSecDeleteUser = @"(DELETE\s+USER)";

        public const string getTable = @"(\w+).sec";
    }
}
