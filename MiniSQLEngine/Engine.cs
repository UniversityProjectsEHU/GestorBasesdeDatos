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
        public abstract void Run(string dbname);
        public abstract string getClass();
    }

    public class ClassParsing
    {
        public string Query(string psentencia, string dbname)
        {
            Query query = Parse(psentencia);
            string a=query.getClass();
            if (a.Equals("select"))
            {
                query.Run(dbname);
               ClassSelect q2 = (ClassSelect)query;
               return q2.getResult();
            }
            else
            {
                return "";
            }
        }
        public Query Parse(string pQuery)
        {
            Match matchselect = Regex.Match(pQuery, Constants.regExTypeSelect);
            if (matchselect.Success)
            {
                return ManageSelect(pQuery);
            }
            Match matchinsert = Regex.Match(pQuery, Constants.regExTypeInsert);

             if (matchinsert.Success)
            {
                return ManageInsert(pQuery);
            }

            Match matchDropTable = Regex.Match(pQuery, Constants.regExTypesDropTable);
             if (matchDropTable.Success)
            {
                return ManageDropTable(pQuery);
            }

            Match matchCreateTable = Regex.Match(pQuery, Constants.regExTypesCreateTable);
             if (matchCreateTable.Success)
            {
                return ManageCreateTable(pQuery);
            }

            Match matchcreatedatabase = Regex.Match(pQuery, Constants.regExTypesCreateDatabase);
            if (matchcreatedatabase.Success)
            {
                return ManageCreateDatabase(pQuery);
            }

            Match matchdropdatabase = Regex.Match(pQuery, Constants.regExTypesDropDatabase);
            if (matchdropdatabase.Success)
            {
                return ManageDropDatabase(pQuery);
            }

            Match matchdelete = Regex.Match(pQuery, Constants.regExTypeDelete);
            if (matchdelete.Success)
            {
                return ManageDelete(pQuery);
            }
            Match matchupdate = Regex.Match(pQuery, Constants.regExTypeUpdate);

            if (matchupdate.Success)
            {
                return ManageUpdate(pQuery);
            }

            //Manejar errores/Excepciones
            return null;
        }

        public ClassUpdate ManageUpdate(string pQuery)
        {
           

            Match Update = Regex.Match(pQuery, Constants.regExpUpdate);
            if (Update.Success)
            {
                string Table = Update.Groups[1].Value;
                string Column = Update.Groups[2].Value;
                string[] ColumnSplit = Column.Split(',');
                string Condition = Update.Groups[3].Value;
                ClassUpdate query = new ClassUpdate(Table, ColumnSplit, Condition);
                return query;
            }
            return null;

        }

        public ClassDelete ManageDelete(string pQuery)
        {
            Match Delete = Regex.Match(pQuery, Constants.regExDelete);
            if (Delete.Success)
            {
                ;
                string Table = Delete.Groups[1].Value;
                string Condition = Delete.Groups[2].Value;
                ClassDelete query = new ClassDelete(Table, Condition);
                return query;
            }
            return null;

        }



        public ClassDropTable ManageDropTable(String pQuery)
        {
            Match matchDropTable = Regex.Match(pQuery, Constants.regExpDropTable);
            if (matchDropTable.Success)
            {
                string name = matchDropTable.Groups[1].Value;
                ClassDropTable query = new ClassDropTable(name);
                return query;
            }
            else
            {
                return null;
            }


        }

        public ClassCreateTable ManageCreateTable(String pQuery)
        {
            Match matchCreateTable = Regex.Match(pQuery, Constants.regExpCreateTable);
            if (matchCreateTable.Success)
            {

                string table = matchCreateTable.Groups[1].Value;
                string values = matchCreateTable.Groups[2].Value;
                string[] myArray = values.Split(',');
                ClassCreateTable query = new ClassCreateTable(table, myArray);
                return query;
            }
            else
            {
                 return null;
            }
        }


        public ClassCreateDatabase ManageCreateDatabase(string pQuery)
        {
            string name = "";
            Match matchcreatedatabase2 = Regex.Match(pQuery, Constants.regExpCreateDatabase);
            if (matchcreatedatabase2.Success)
            {
                name = matchcreatedatabase2.Groups[1].Value;

            }
            ClassCreateDatabase query = new ClassCreateDatabase(name);
            return query;
        }

        public ClassDropDatabase ManageDropDatabase(string pQuery)
        {
            string name = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regExpDropDatabase);
            if (matchdropdatabase2.Success)
            {
                name = matchdropdatabase2.Groups[1].Value;

            }
            ClassDropDatabase query = new ClassDropDatabase(name);
            return query;
        }


        public ClassInsert ManageInsert(String pQuery)
        {
            //public const String regExInsert = @"INSERT\s+INTO\s+(\w+)\s+VALUES\s+\(([^\)]+)\);";
            Match match = Regex.Match(pQuery, Constants.regExInsert);
            if (match.Success)
            {
                string table = match.Groups[1].Value;
                string values = match.Groups[2].Value;
                string[] myArray = values.Split(',');
                ClassInsert query = new ClassInsert(table, myArray);
                return query;
            }

            return null;
        }
        public ClassSelect ManageSelect(string pQuery)
        {
            ClassSelect query;
            Match matchselect2 = Regex.Match(pQuery, Constants.regExSelect);
            if (matchselect2.Success)
            {
                string columns = matchselect2.Groups[1].Value;
                string table = matchselect2.Groups[2].Value;
                string condition = matchselect2.Groups[3].Value;
                string[] columnssplit = columns.Split(',');
                query = new ClassSelect(columnssplit, table, condition);
                return query;

            }
            return null;
        }
    }

    public class Database
    {
        private string dbname;
        private static Database database;
        private Database(string dbname)
        {
            this.dbname = dbname;
        }
        public Database getDatabase(string nombre)
        {
            if (database==null)
            {
                database = new Database(nombre);
            }
            return database;
        }

        public string getNombre()
        {
            return dbname;
        }

        public string Query(string psentencia)
        {
            ClassParsing c = new ClassParsing();
            return c.Query(psentencia,dbname);
        }
    }
}
