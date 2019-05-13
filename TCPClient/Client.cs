using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using MiniSQLEngine;

namespace TCPClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string argPrefixIp = "ip=";
            const string argPrefixPort = "port=";
            

            string ip = null;
            int port = 0;
            foreach (string arg in args)
            {
                if (arg.StartsWith(argPrefixIp)) ip = arg.Substring(argPrefixIp.Length);
                else if (arg.StartsWith(argPrefixPort)) port = int.Parse(arg.Substring(argPrefixPort.Length));
            }
            if (ip == null || port == 0)
            {
                Console.WriteLine("ERROR. Usage: TCPClient ip=<ip> port=<port>");
                return;
            }

            using (TcpClient client = new TcpClient(ip, port))
            {
                NetworkStream networkStream = client.GetStream();
                byte[] endMessage = Encoding.ASCII.GetBytes("END");
                byte[] inputBuffer = new byte[1024];
                byte[] outputBuffer = new byte[1024];
                int readBytes = 0;
                int cont = 0;
                while (cont == 0)
                {
                    Console.Write("Enter your user: ");
                    string user = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    string pass = Console.ReadLine();
                    Console.Write("Enter the name of the database: ");
                    string datab = Console.ReadLine();

                     outputBuffer = Encoding.ASCII.GetBytes("<Open Database=\"" + datab + "\" User=\"" + user + "\" Password=\"" + pass + "\"/>");
                   
               


                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                     readBytes = networkStream.Read(inputBuffer, 0, 1024);
                    //Console.WriteLine("Response received: " + Encoding.ASCII.GetString(inputBuffer, 0, readBytes));

                    //Thread.Sleep(2000);
                            if(Encoding.ASCII.GetString(inputBuffer, 0, readBytes)== "<Success/>")
                            {
                                 Console.WriteLine("Database opened");
                                cont++;
                            }
                 }
                 
                while (Encoding.ASCII.GetString(inputBuffer, 0, readBytes) != "<Close/>")
                {
                    string q;
                    Console.Write("Enter the query: ");
                    q = Console.ReadLine();
                    if (q.ToLower() == "exit")
                    {
                        outputBuffer = Encoding.ASCII.GetBytes("END");
                        networkStream.Write(endMessage, 0, endMessage.Length);                                              
                        Environment.Exit(0);
                    }
                    else
                    {
                        outputBuffer = Encoding.ASCII.GetBytes("<Query>" + q + "</Query>");
                        networkStream.Write(outputBuffer, 0, outputBuffer.Length);
                    }
                    
                    //Aqui enviamos

                    readBytes = networkStream.Read(inputBuffer, 0, 1024);
                    //Console.WriteLine("Response received: " + Encoding.ASCII.GetString(inputBuffer, 0, readBytes));
                    string answer = Encoding.ASCII.GetString(inputBuffer, 0, readBytes);
                    Match matchClientAnswer= Regex.Match(answer, Constants.regExClientAnswer);
                    if (matchClientAnswer.Success)
                    {
                        Console.WriteLine(matchClientAnswer.Groups[1].Value);
                    }
                }
                //Aqui termina
                networkStream.Write(endMessage, 0, endMessage.Length);
                

            }
        }
    }
}
