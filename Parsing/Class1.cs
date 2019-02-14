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
            if (matchselect.Success)
            {
                manageSelect(pQuery);
            }


          
        }

       

        public Query manageSelect(String pQuery)
        {
            ClassSelect query = new ClassSelect();
            return query;
      
        }
    }
}
