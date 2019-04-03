using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class SecRevoke : Query
    {
        private string privilege_type, table, security_profile;
        private string result;

        public SecRevoke(string pprivilege_type, string ptable, string psecurity_profile)
        {
            privilege_type = pprivilege_type;
            table = ptable;
            security_profile = psecurity_profile;
        }
        public override string getClass()
        {
            return "revoke";
        }

        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            string pathProfiles = @"..\\..\\..\\data\\" + dbname + "\\profiles\\" + security_profile;
            string pathUssers = @"..\\..\\..\\data\\" + dbname + "\\" + table + ".sec";
            if (File.Exists(pathProfiles) == false)
            {
                result = Constants.SecurityProfileDoesNotExist;
            }
            else
            {
                if (File.Exists(pathUssers) == false)
                {
                    result = Constants.TableDoesNotExist;
                }
                else
                {
                    String[] lineasSec = System.IO.File.ReadAllLines(pathUssers);
                    int contar = 0;
                    int contaroficial = -1;
                    foreach (string actual in lineasSec)
                    {
                        string[] actualSplit = actual.Split(',');
                        if (actualSplit[0].Contains(security_profile))
                        {
                            contaroficial = contar;
                        }
                        else
                        {
                            contar++;
                        }
                    }
                    if (contaroficial != -1)
                    {
                        string linea = lineasSec[contaroficial];
                        string[] lineaSplit = linea.Split(',');
                        string profile = lineaSplit[0];

                        string[] privi = lineaSplit[1].Split('/');
                        Boolean lotiene = false;
                        int contador = 0;
                        int locontado = -1;
                        foreach (string actual in privi)
                        {
                            if (actual == privilege_type.ToUpper())
                            {
                                lotiene = true;
                                locontado = contador;
                            }
                            contador++;
                        }
                        if (locontado != -1)
                        {
                            ArrayList nuevosprivi = new ArrayList();
                            privi[locontado] = null;
                            foreach (string ahora in privi)
                            {
                                if (ahora != null)
                                {
                                    nuevosprivi.Add(ahora);
                                }
                            }
                            string lineaprivi = "";
                            foreach (string ahora in nuevosprivi)
                            {
                                lineaprivi = lineaprivi + ahora + "/";
                            }
                            lineaprivi = lineaprivi.TrimEnd('/');

                            string milinea = security_profile + "," + lineaprivi;
                            lineasSec[contaroficial] = milinea;
                            using (StreamWriter stream3 = File.CreateText(pathUssers))
                            {
                                foreach (string ahora in lineasSec)
                                {
                                    stream3.WriteLine(ahora);
                                }
                            }
                            result = Constants.SecurityPrivilegeRevoked;
                        }
                        result = Constants.SecurityPrivilegeRevoked;
                    }
                }
            }
        }
    }
}
