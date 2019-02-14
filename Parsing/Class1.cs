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
        public void parse(String pQuery)
        {
            Match matchselect = Regex.Match(pQuery,Constants.regExSelect);
            Match matchinsert = Regex.Match(pQuery, Constants.regExTypeInsert);
            if (matchselect.Success)
            {
                manageSelect(pQuery);
            }

            else if (matchinsert.Success)
            {
                ManageInsert(pQuery);
            }

          
        }

       

        public Query manageSelect(String pQuery)
        {
            ClassSelect query = new ClassSelect();
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
    }
}
