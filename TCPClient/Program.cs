using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

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

                byte[] outputBuffer = Encoding.ASCII.GetBytes("Do you want to marry me????");
                byte[] inputBuffer = new byte[1024];
                byte[] endMessage = Encoding.ASCII.GetBytes("END");

                for (int i = 0; i < 5; i++)
                {
                    networkStream.Write(outputBuffer, 0, outputBuffer.Length);

                    int readBytes = networkStream.Read(inputBuffer, 0, 1024);
                    Console.WriteLine("Response received: " + Encoding.ASCII.GetString(inputBuffer,0,readBytes));

                    Thread.Sleep(2000);
                }
                networkStream.Write(endMessage, 0, endMessage.Length);
            }
        }
    }
}
