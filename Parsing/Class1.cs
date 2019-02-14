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
    }
}
