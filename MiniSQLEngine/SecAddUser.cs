using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class SecAddUser : Query
    {
        private string user;
        private string pw;
        private string profile;
        private string result;

        public SecAddUser(string user, string pw, string profile)
        {
            this.user = user;
            this.pw = pw;
            this.profile = profile;
        }

        public override string getClass()
        {
            return "AddUser";
        }

        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            string pathfileDATA = @"..//..//..//data//" + dbname + "//profiles//" + profile + ".pf";

            if (!File.Exists(pathfileDATA))
            {
                result = Constants.SecurityProfileDoesNotExist;
            }
            using (StreamWriter file = File.AppendText(pathfileDATA))
            {
                //Data added to the document
                file.WriteLine(user + " " + pw);
                file.Close();
                result = Constants.SecurityUserCreated;
            }
        }
    }
}
