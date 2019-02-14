using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQLEngine;
using System.Text.RegularExpressions;
using Parsing;

namespace Parsing
{
    public class Class1
    {
        public void Parse(string pQuery)
        {
            Match matchselect = Regex.Match(pQuery,Constants.regExTypeSelect);
            Match matchcreatedatabase = Regex.Match(pQuery, Constants.regExTypesCreateDatabase);
            Match matchdropdatabase = Regex.Match(pQuery, Constants.regExTypesDropDatabase);
            Match matchDropTable = Regex.Match(pQuery, Constants.regExTypesDropTable);
            Match matchCreateTable = Regex.Match(pQuery, Constants.regExTypesCreateTable);
            Match matchdelete = Regex.Match(pQuery, Constants.regExTypeDelete);
            Match matchupdate = Regex.Match(pQuery, Constants.regExTypeUpdate);


            Match matchinsert = Regex.Match(pQuery, Constants.regExTypeInsert);
            if (matchselect.Success)
            {
                ManageSelect(pQuery);
            }
            else if (matchinsert.Success)
            {
                ManageInsert(pQuery);
            }
            else if (matchDropTable.Success)
            {
                ManageDropTable(pQuery);
            }
            else if (matchCreateTable.Success)
            {
                ManageCreateTable(pQuery);
            }

            else if (matchcreatedatabase.Success)
            {
                ManageCreateDatabase(pQuery);
            }

            else if (matchdropdatabase.Success)
            {
                ManageDropDatabase(pQuery);
            }
           
             else if (matchdelete.Success)
            {
                ManageDelete(pQuery);
            }
            else if (matchselect.Success)
            {
                ManageUpdate(pQuery);
            }


        }

        public Query ManageUpdate(string pQuery)
        {
            Match Update = Regex.Match(pQuery, Constants.regExpUpdate);
            if(Update.Success)
            {
                string Table = Update.Groups[0].Value;
                string Column = Update.Groups[1].Value;
                string[] ColumnSplit = Column.Split(',');
                string Condition = Update.Groups[2].Value;
                ClassUpdate query = new ClassUpdate(Table,ColumnSplit,Condition);
                return query;
            }
            return null;
           
        }

        public Query ManageDelete(string pQuery)
        {
            Match Update = Regex.Match(pQuery, Constants.regExpUpdate);
            if (Update.Success)
            {
                ;
                string Table = Update.Groups[1].Value;
                string Condition = Update.Groups[2].Value;
                ClassDelete query = new ClassDelete(Table,Condition);
                return query;
            }
            return null;
           
        }

        

        public Query ManageDropTable(String pQuery)
        {
            Match matchDropTable = Regex.Match(pQuery, Constants.regExpDropTable);
            if (matchDropTable.Success)
            {
                string name = matchDropTable.Groups[1].Value;
                DropTable query = new DropTable(name);
                return query;
            }
            else
            {
                return null;
            }


        }

        public Query ManageCreateTable(String pQuery)
        {
            Match matchCreateTable = Regex.Match(pQuery, Constants.regExpCreateTable);
            if (matchCreateTable.Success)
            {
                CreateTable query = new CreateTable();
                return query;
            }
            else
            {
                return null;
            }
        }
            

        public Query ManageCreateDatabase(string pQuery)
        {
            string name="";
            Match matchcreatedatabase2 = Regex.Match(pQuery, Constants.regExpCreateDatabase);
            if (matchcreatedatabase2.Success)
            {
                name = matchcreatedatabase2.Groups[1].Value;

            }
            CreateDatabase query = new CreateDatabase(name);
            return query;
        }

        public Query ManageDropDatabase(string pQuery)
        {
            string name = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regExpDropDatabase);
            if (matchdropdatabase2.Success)
            {
                name = matchdropdatabase2.Groups[1].Value;

            }
            DropDatabase query = new DropDatabase(name);
            return query;
        }


        public Query ManageInsert(String pQuery)
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
        public Query ManageSelect(string pQuery)
        {
            ClassSelect query;
            Match matchselect2 = Regex.Match(pQuery,Constants.regExSelect);
            if (matchselect2.Success)
            {
               string columns= matchselect2.Groups[1].Value;
               string table = matchselect2.Groups[2].Value;
               string condition = matchselect2.Groups[3].Value;
               string[] columnssplit = columns.Split(',');
                query = new ClassSelect(columnssplit,table,condition);
                return query;

            }
            return null;
        }
    }
}
