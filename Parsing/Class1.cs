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
        public void Parse(String pQuery)
        {
            Match matchselect = Regex.Match(pQuery,Constants.regExSelect);
            Match matchcreatedatabase = Regex.Match(pQuery, Constants.regExTypesCreateDatabase);
            Match matchdropdatabase = Regex.Match(pQuery, Constants.regExTypesDropDatabase);
            Match matchDropTable = Regex.Match(pQuery, Constants.regExTypesDropTable);
            Match matchCreateTable = Regex.Match(pQuery, Constants.regExTypesCreateTable);
            if (matchselect.Success)
            {
                ManageSelect(pQuery);
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


        }

       

        public Query ManageSelect(string pQuery)
        {
            ClassSelect query = new ClassSelect();
            return query;

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

    }
}
