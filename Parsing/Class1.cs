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
            Match matchdelete = Regex.Match(pQuery, Constants.regExTypeDelete);
             else if (matchdelete.Success)
            {
                ManageDelete(pQuery);
            }
            Match matchupdate = Regex.Match(pQuery, Constants.regExTypeUpdate);
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
                String Columna = Update.Groups[0].Value;
                String Tabla = Update.Groups[1].Value;
                String Condicion = Update.Groups[2].Value;
                ClassUpdate query = new ClassUpdate();

            }
            return query;
        }

        public Query ManageDelete(string pQuery)
        {
            Match Update = Regex.Match(pQuery, Constants.regExpUpdate);
            if (Update.Success)
            {
                String Columna = Update.Groups[1].Value;
                String Tabla = Update.Groups[2].Value;
                String Condicion = Update.Groups[3].Value;
                ClassUpdate query = new ClassUpdate();

            }
            return query;
        }

        public Query manageSelect(String pQuery)
        {
            ClassSelect query = new ClassSelect();
            return query;
      
        }
    }
}
