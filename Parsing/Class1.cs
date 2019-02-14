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

            if (matchselect.Success)
            {
                ManageSelect(pQuery);
            }

            Match matchDropTable = Regex.Match(pQuery, Constants.regExTypesDropTable);
            else if (matchDropTable.Success)
            {
                ManageDropTable(pQuery);
            }

            Match matchCreateTable = Regex.Match(pQuery, Constants.regExTypesCreateTable);
            else if (matchCreateTable.Success)
            {
                ManageCreateTable(pQuery);
            }

            if (matchcreatedatabase.Success)
            {
                ManageCreateDatabase(pQuery);
            }

            if (matchdropdatabase.Success)
            {
                ManageDropDatabase(pQuery);
            }


        }

       


        public Query ManageSelect(String pQuery)
        {
            ClassSelect query = new ClassSelect();
            return query;

        }

        public Query ManageDropTable(String pQuery)
        {
            Match matchDropTable = Regex.Match(pQuery, Constants.regExpDropTable);
            if (matchDropTable.Success)
            {
                DropTable query = new DropTable();
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
                ClassTable query = new ClassTable();
                return query;
            }
            else
            {
                return null;
            }

      
        }

        public Query ManageCreateDatabase(String pQuery)
        {
            CreateDatabase query = new CreateDatabase();
            Match matchcreatedatabase2 = Regex.Match(pQuery, Constants.regExpCreateDatabase);
            if (matchcreatedatabase2.Success)
            {
                //matchcreatedatabase2
            }


            return query;
        }

        public Query ManageDropDatabase(String pQuery)
        {
            DropDatabase query = new DropDatabase();
            return query;
        }

    }
}
