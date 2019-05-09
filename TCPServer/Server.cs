using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using MiniSQLEngine;

namespace TCPServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string argPrefixPort = "port=";

            int port = 0;
            foreach (string arg in args)
            {
                if (arg.StartsWith(argPrefixPort)) port = int.Parse(arg.Substring(argPrefixPort.Length));
            }
            if (port == 0)
            {
                Console.WriteLine("ERROR. Usage: TCPClient ip=<ip> port=<port>");
                return;
            }

            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            Console.WriteLine("Server listening for clients");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connection accepted");

                var childSocketThread = new Thread(() =>
                {
                    byte[] inputBuffer = new byte[1024];
                    Database db = null;

                    NetworkStream networkStream = client.GetStream();

                    //Read message from the client
                    int size = networkStream.Read(inputBuffer, 0, 1024);
                    string request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                    while (request != "END")
                    {
                        
                        string answer = "";
                        Console.WriteLine("Request received: " + request);
                        Match matchopendb = Regex.Match(request, Constants.regExOpenDatabase);
                        Match matchexQuery = Regex.Match(request, Constants.regExQuery);

                        if (matchopendb.Success)
                        {
                            
                             db = new Database(matchopendb.Groups[1].Value, matchopendb.Groups[2].Value, matchopendb.Groups[3].Value);
                            string res = db.getRes();
                            if (res == Constants.CreateDatabaseSuccess)
                            {
                                answer = "<Success/>";
                            }
                            else if (res == Constants.OpenDatabaseSuccess)
                            {
                                answer = "<Success/>";

                            }
                            else if (res == Constants.SecurityNotSufficientPrivileges + "Not Admin")
                            {
                                answer = "<Error>The database doesn’t exist</Error>";
                            }
                            
                            else
                            {
                                answer = "<Error>Incorrect login</Error>";
                            }
                        }
                        else if (matchexQuery.Success)
                        {
                            string query = matchexQuery.Groups[1].Value;
                            answer=db.Query(query,db);
                            //We look if the Query is Select
                            //if (answer.Contains("{"))
                            //{
                            //    Match matchselect = Regex.Match(request, Constants.regExSelectanswer);
                            //    if (matchselect.Success)
                            //    {
                            //        string[] atribs = matchselect.Groups[1].Value.Split(',');
                            //        int length = matchselect.Length;
                            //        List<string> values = new List<string>();
                            //        for (int i = 2; i < length; i++)
                            //        {
                            //            values.Add(matchselect.Groups[i].Value);
                            //        }
                            //        answer = "<Answer>\n\t<Columns>";
                            //        foreach (string atrib in atribs)
                            //        {

                            //            answer = answer + "\n\t\t<Column>" + atrib + "</Column>";
                            //        }
                            //        answer = answer + "\n\t</Columns>\n\t<Rows>";

                            //        foreach (string value in values)
                            //        {
                            //            answer = answer + "\n\t\t<Row>";
                            //            string[] splittedvalues = value.Split(',');
                            //            foreach (string splittedvalue in splittedvalues)
                            //            {
                            //                answer = answer + "\n\t\t\t<Value>" + splittedvalue + "</Value>";
                            //            }
                            //            answer = answer + "\n\t\t</Row>";
                            //        }
                            //        answer = answer + "\n\t</Rows>\n</Answer>";
                            //    }

                            //}
                            //else
                            //{
                            //    answer = "<Answer>" + answer + "</Answer>";
                            //}
                            answer = "<Answer>" + answer + "</Answer>";
                        }

                        byte[] outputBuffer = Encoding.ASCII.GetBytes(answer);
                        networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                        size = networkStream.Read(inputBuffer, 0, 1024);
                        request = Encoding.ASCII.GetString(inputBuffer, 0, size);
                    }
                    client.Close();
                });
                childSocketThread.Start();
            }
        }
    }
}
