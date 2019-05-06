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
        public string Query(string psentencia, string dbname,Database pDB)
        {
            Boolean existTablePrivileges = false;
            try
            {
                Query query = Parse(psentencia);
                string a = query.getClass();
                 if (pDB.getUser() == "admin")
                {

                    query.Run(dbname);
                    return query.getResult();
                }
                else if (a.Equals("select"))
                {
                    Match matchtableselect = Regex.Match(psentencia, @"SELECT\s+.+\s+FROM\s+(\w+)");
                    string table = matchtableselect.Groups[1].Value;
                    List<TablePrivileges> userprivileges = pDB.GetTablePrivileges();
                    foreach (TablePrivileges tableprv in userprivileges)
                    {
                        if (tableprv.getTableName() == table)
                        {
                            if (tableprv.getTablePrivileges().Contains("SELECT"))
                            {
                                existTablePrivileges = true;
                                query.Run(dbname);
                                ClassSelect q2 = (ClassSelect)query;
                                return q2.getResult();
                            }
                            else
                            {
                                return Constants.SecurityNotSufficientPrivileges;
                            }
                        }
                      
                    }


                   
                }

                else if (a.Equals("delete"))
                {
                    Match matchtableselect = Regex.Match(psentencia, @"DELETE\s+FROM\s+(\w+)");
                    string table = matchtableselect.Groups[1].Value;
                    List<TablePrivileges> userprivileges = pDB.GetTablePrivileges();
                    foreach (TablePrivileges tableprv in userprivileges)
                    {
                        if (tableprv.getTableName() == table)
                        {
                            if (tableprv.getTablePrivileges().Contains("DELETE"))
                            {
                                existTablePrivileges = true;
                                query.Run(dbname);
                                ClassDelete q2 = (ClassDelete)query;
                                return q2.getResult();
                            }
                            else
                            {
                                return Constants.SecurityNotSufficientPrivileges;
                            }
                        }
                       
                    }


                }

                else if (a.Equals("insert"))
                {
                    Match matchtableselect = Regex.Match(psentencia, @"INSERT\s+INTO\s+(\w+)");
                    string table = matchtableselect.Groups[1].Value;
                    List<TablePrivileges> userprivileges = pDB.GetTablePrivileges();
                    foreach (TablePrivileges tableprv in userprivileges)
                    {
                        if (tableprv.getTableName() == table)
                        {
                            if (tableprv.getTablePrivileges().Contains("INSERT"))
                            {
                                existTablePrivileges = true;
                                query.Run(dbname);
                                ClassInsert q2 = (ClassInsert)query;
                                return q2.getResult();
                            }
                            else
                            {
                                return Constants.SecurityNotSufficientPrivileges;
                            }
                        }
                       
                    }


                }

                else if (a.Equals("update"))
                {
                    Match matchtableselect = Regex.Match(psentencia, @"UPDATE\s+(\w+)");
                    string table = matchtableselect.Groups[1].Value;
                    List<TablePrivileges> userprivileges = pDB.GetTablePrivileges();
                    foreach (TablePrivileges tableprv in userprivileges)
                    {
                        if (tableprv.getTableName() == table)
                        {
                            if (tableprv.getTablePrivileges().Contains("UPDATE"))
                            {
                                existTablePrivileges = true;
                                query.Run(dbname);
                                ClassUpdate q2 = (ClassUpdate)query;
                                return q2.getResult();
                            }
                            else
                            {
                                return Constants.SecurityNotSufficientPrivileges;
                            }
                        }
                      
                    }


                }

                if (!existTablePrivileges)
                {
                    return Constants.SecurityNotSufficientPrivileges;
                }
                return null;
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
               SecAddUser query = new SecAddUser(user, pass, prof);
                return query;
            }
            return null;
           
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
                table = matchdropdatabase2.Groups[2].Value;
                perfil = matchdropdatabase2.Groups[3].Value;
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
                table = matchdropdatabase2.Groups[2].Value;
                perfil = matchdropdatabase2.Groups[3].Value;
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
        private static string dbname;
        private static string user;
        private string res;
        private static string[] privileges;
        private static List<TablePrivileges> userprivileges;
        private static string profile="notfound";
        public Database(string name, string pUser,string pPass)
        {
            dbname = name;
            user = pUser;
            res = init(name, pUser, pPass);
            dbname = name;
            

        }
        public static String init(string name, string pUser, string pPassword)
        {
            string res;
            
            if (!Directory.Exists("..//..//..//data//" + name))
            {
                if (pUser.Equals("admin") && pPassword.Equals("admin"))
                {
                    res = Constants.CreateDatabaseSuccess;
                    ClassCreateDatabase dbc = new ClassCreateDatabase(name);
                    dbc.Run(name);
                    
                    
                    return res;
                }
                else 
                {
                    res = Constants.SecurityNotSufficientPrivileges + "Not Admin";
                    return res;
                }
            }
            if (pUser == "admin" && pPassword == "admin")
            {
                privileges = new String[4] { "DELETE", "INSERT", "SELECT", "UPDATE" };
                res = Constants.OpenDatabaseSuccess;
                return res;

            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(@"..//..//..//data//" + dbname + "//profiles");
                FileInfo[] files = di.GetFiles();

                foreach (var file in files)
                {
                    string line;
                    string tempf = file.Name;
                    using (StreamReader sr = new StreamReader(@"..//..//..//data//" + dbname + "//profiles//"+tempf))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts[0].Equals(pUser) && parts[1].Equals(pPassword))
                            {
                                res = Constants.OpenDatabaseSuccess;
                                profile = tempf;
                                //We identify all the user's privileges
                                
                                string[] dirs = Directory.GetFiles(@"..//..//..//data//"+dbname);
                                int size = dirs.Length;
                                userprivileges = new List<TablePrivileges>();
                                foreach (string filesec in dirs)
                                {
                                    if (filesec.Contains(".sec"))
                                    {

                                    
                                    string[] temppriv = File.ReadAllLines(filesec);
                                    foreach (string prfsec in temppriv)
                                    {
                                        string[] splittedprof = prfsec.Split(',');
                                        if (splittedprof[0]+".pf" == profile)
                                        {
                                            string[] privilegesprf = splittedprof[1].Split('/');
                                            Match a = Regex.Match(filesec, Constants.getTable);
                                            userprivileges.Add(new TablePrivileges(a.Groups[1].Value, privilegesprf));
                                        }
                                    }


                                }
                                }
                                return res;
                            }
                        }
                    }
                }
                res = Constants.SecurityIncorrectLogin; 
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
        public string[] getPrivilegesAdmin()
        {
            return privileges;
        }
        public List<TablePrivileges> GetTablePrivileges()
        {
            return userprivileges;
        }

        public string Query(string psentencia,Database pDB)
        {
            ClassParsing c = new ClassParsing();
            


            return c.Query(psentencia,dbname,pDB);
        }
    }
    public class TablePrivileges
    {
        private string table_name;
        private List<string> table_privileges;
        public TablePrivileges(string table_name, string[] privileges)
        {
            table_privileges = new List<string>();
            this.table_name = table_name;
            foreach (string privilege in privileges)
            {
                table_privileges.Add(privilege);
            }
        }
        public string getTableName()
        {
            return table_name;
        }

        public List<String> getTablePrivileges()
        {
            return table_privileges;
        }
    }
}
