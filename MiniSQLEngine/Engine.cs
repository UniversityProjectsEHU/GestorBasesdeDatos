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
        public abstract string getResult();
    }

    public class ClassParsing
    {
        public string Query(string psentencia, string dbname)
        {
            try
            {
                Query query = Parse(psentencia);
                string a = query.getClass();
                if (a.Equals("select"))
                {
                    query.Run(dbname);
                    ClassSelect q2 = (ClassSelect)query;
                    return q2.getResult();
                }
                else
                {

                    query.Run(dbname);
                    return query.getResult();
                }
            }
            catch(Exception e)
            {
                string errorreg = "Your query is not valid";
                return errorreg;
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

            Match matchSecAddUser = Regex.Match(pQuery, Constants.regExTypeSecAddUser);

            if (matchSecAddUser.Success)
            {
                return ManageSecAddUser(pQuery);
            }

            Match matchSecDeleteUser = Regex.Match(pQuery, Constants.regExTypeSecDeleteUser);

            if (matchSecDeleteUser.Success)
            {
                return ManageSecDeleteUser(pQuery);
            }
            Match matchSecGrant = Regex.Match(pQuery, Constants.regExTypeSecGrant);

            if (matchSecGrant.Success)
            {
                return ManageSecGrant(pQuery);
            }
            Match matchSecRevoke = Regex.Match(pQuery, Constants.regExTypeSecRevoke);

            if (matchSecRevoke.Success)
            {
                return ManageSecRevoke(pQuery);
            }
            Match matchSecCreateProfile = Regex.Match(pQuery, Constants.regExTypeSecCreateProfile);

            if (matchSecCreateProfile.Success)
            {
                return ManageSecCreateProfile(pQuery);
            }
            Match matSecDropProfile = Regex.Match(pQuery, Constants.regExTypeSecDropProfile);

            if (matSecDropProfile.Success)
            {
                return ManageSecDropProfile(pQuery);
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
                foreach (String st in myArray)
                {
                    st.Trim();
                }
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
            Match match2 = Regex.Match(pQuery, Constants.regExInsert2);
            if (match.Success)
            {
                string table = match.Groups[1].Value;
                string values = match.Groups[2].Value;
                string[] myArray = values.Split(',');
                ClassInsert query = new ClassInsert(table, myArray, null);
                return query;
            }
            else if (match2.Success)
            {
                string table = match2.Groups[1].Value;
                string atributes = match2.Groups[2].Value;
                string[] myArray2 = atributes.Split(',');
                string values = match2.Groups[3].Value;
                string[] myArray = values.Split(',');
                
                ClassInsert query = new ClassInsert(table, myArray, myArray2);
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
                query = new ClassSelect(columnssplit, table, condition,pQuery);
                return query;

            }
            else
            {
                Match matchselectV3= Regex.Match(pQuery, Constants.regExSelect2);
                if (matchselectV3.Success)
                {
                    string columns = matchselectV3.Groups[1].Value;
                    string table = matchselectV3.Groups[2].Value;
                    string[] columnssplit = columns.Split(',');
                    query = new ClassSelect(columnssplit, table, "", pQuery);
                    return query;
                }
            }
            return null;
        }

        public SecAddUser ManageSecAddUser(string pQuery)
        {
            string user = "";
            string pass = "";
            string prof = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regSecAddUser);
            if (matchdropdatabase2.Success)
            {
               user = matchdropdatabase2.Groups[1].Value;
               pass = matchdropdatabase2.Groups[2].Value;
               prof = matchdropdatabase2.Groups[3].Value;
            }
            SecAddUser query = new SecAddUser(user,pass,prof);
            return query;
        }
        public SecDeleteUser ManageSecDeleteUser(string pQuery)
        {
            string user = "";
            string pass = "";
            string prof = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regSecDeleteUser);
            if (matchdropdatabase2.Success)
            {
                user = matchdropdatabase2.Groups[1].Value;
            }
            SecDeleteUser query = new SecDeleteUser(user);
            return query;
        }
        public SecGrant ManageSecGrant(string pQuery)
        {
            string priv = "";
            string table = "";
            string perfil = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regSecGrant);
            if (matchdropdatabase2.Success)
            {
                priv = matchdropdatabase2.Groups[1].Value;
                table = matchdropdatabase2.Groups[1].Value;
                perfil = matchdropdatabase2.Groups[1].Value;
            }
            SecGrant query = new SecGrant(priv,table,perfil);
            return query;
        }
        public SecRevoke ManageSecRevoke(string pQuery)
        {
            string priv = "";
            string table = "";
            string perfil = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regSecRevoke);
            if (matchdropdatabase2.Success)
            {
                priv = matchdropdatabase2.Groups[1].Value;
                table = matchdropdatabase2.Groups[1].Value;
                perfil = matchdropdatabase2.Groups[1].Value;
            }
            SecRevoke query = new SecRevoke(priv, table, perfil);
            return query;
        }
        public SecCreateProfile ManageSecCreateProfile(string pQuery)
        {
            string priv = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regSecCreateProfile);
            if (matchdropdatabase2.Success)
            {
                priv = matchdropdatabase2.Groups[1].Value;
            }
            SecCreateProfile query = new SecCreateProfile(priv);
            return query;
        }
        public SecDropProfile ManageSecDropProfile(string pQuery)
        {
            string priv = "";
            Match matchdropdatabase2 = Regex.Match(pQuery, Constants.regSecDropProfile);
            if (matchdropdatabase2.Success)
            {
                priv = matchdropdatabase2.Groups[1].Value;
            }
            SecDropProfile query = new SecDropProfile(priv);
            return query;
        }
    }

    public class Database
    {
        private string dbname;
        private string user;
        private string res;
        private static string[] privileges;
        public Database(string name, string pUser,string pPass)
        {
            dbname = name;
            user = pUser;
            res = init(name, pUser, pPass);

        }
        public static String init(string name, string pUser, string pPassword)
        {
            string res;
            if (!Directory.Exists("..//..//..//data//" + name))
            {
                if (pUser.Equals("admin") && pPassword.Equals("admin"))
                {
                    res = "adminCreateDB";
                    ClassCreateDatabase dbc = new ClassCreateDatabase(name);
                    dbc.Run(name);
                    privileges = new String[4] { "DELETE", "INSERT", "SELECT", "UPDATE" };
                    return res;
                }
                else
                {
                    res = "notAdmin";
                    return res;
                }
            }
            else
            {
                string pathfile = @"..//..//..//data//" + name + "//profiles";

                foreach (string file in Directory.EnumerateFiles(pathfile, "*.pf"))
                {
                    string line;
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts[0].Equals(pUser) && parts[1].Equals(pPassword))
                            {
                                res = "UserOpen";
                                return res;
                            }
                        }
                    }
                }
                res = "notUserOrPassw";
                return res;
            }
        }
       
        public string getNombre()
        {
            return dbname;
        }

        public string getUser()
        {
            return user;
        }
        public string getRes()
        {
            return res;
        }
        public string Query(string psentencia)
        {
            ClassParsing c = new ClassParsing();
            //String eltipo = "";
            //Match matchselect = Regex.Match(psentencia, Constants.regExTypeSelect);
            //if (matchselect.Success)
            //{
            //    eltipo="SELECT";
            //}
            //Match matchinsert = Regex.Match(psentencia, Constants.regExTypeInsert);

            //if (matchinsert.Success)
            //{
            //    eltipo = "INSERT";
            //}

            //Match matchDropTable = Regex.Match(psentencia, Constants.regExTypesDropTable);
            //if (matchDropTable.Success)
            //{
            //    eltipo = "DROP TABLE";
            //}

            //Match matchCreateTable = Regex.Match(psentencia, Constants.regExTypesCreateTable);
            //if (matchCreateTable.Success)
            //{
            //    eltipo = "CREATE TABLE";
            //}

            //Match matchcreatedatabase = Regex.Match(psentencia, Constants.regExTypesCreateDatabase);
            //if (matchcreatedatabase.Success)
            //{
            //    eltipo = "CREATE DATABASE";
            //}

            //Match matchdropdatabase = Regex.Match(psentencia, Constants.regExTypesDropDatabase);
            //if (matchdropdatabase.Success)
            //{
            //    eltipo = "DROP DATABASE";
            //}

            //Match matchdelete = Regex.Match(psentencia, Constants.regExTypeDelete);
            //if (matchdelete.Success)
            //{
            //    eltipo = "DROP TAB";
            //}
            //Match matchupdate = Regex.Match(psentencia, Constants.regExTypeUpdate);

            //if (matchupdate.Success)
            //{
            //    eltipo = "UPDATE";
            //}

            //Match matchSecAddUser = Regex.Match(psentencia, Constants.regExTypeSecAddUser);

            //if (matchupdate.Success)
            //{
            //    eltipo = "ADD USER";
            //}

            //Match matchSecDeleteUser = Regex.Match(psentencia, Constants.regExTypeSecDeleteUser);

            //if (matchupdate.Success)
            //{
            //    eltipo = "DELETE USER";
            //}
            //Match matchSecGrant = Regex.Match(psentencia, Constants.regExTypeSecGrant);

            //if (matchupdate.Success)
            //{
            //    eltipo = "GRANT";
            //}
            //Match matchSecRevoke = Regex.Match(psentencia, Constants.regExTypeSecRevoke);

            //if (matchupdate.Success)
            //{
            //    eltipo = "REVOKE";
            //}
            //Match matchSecCreateProfile = Regex.Match(psentencia, Constants.regExTypeSecCreateProfile);

            //if (matchupdate.Success)
            //{
            //    eltipo = "CREATE PROFILE";
            //}
            //Match matSecDropProfile = Regex.Match(psentencia, Constants.regExTypeSecDropProfile);

            //if (matchupdate.Success)
            //{
            //    eltipo = "DROP PROFILE";
            //}


            return c.Query(psentencia,dbname);
        }
    }
}
