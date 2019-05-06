using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

                    NetworkStream networkStream = client.GetStream();

                    //Read message from the client
                    int size = networkStream.Read(inputBuffer, 0, 1024);
                    string request = Encoding.ASCII.GetString(inputBuffer, 0, size);

                    while (request != "END")
                    {
                        Console.WriteLine("Request received: " + request);

                        byte[] outputBuffer = Encoding.ASCII.GetBytes("My answer is NO");
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
