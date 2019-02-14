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
            if (matchselect.Success)
            {
                ManageSelect(pQuery);
            }


          
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
