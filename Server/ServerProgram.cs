using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string backResponse;
            Random rastgele = new Random();

            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1302);
            listener.Start();


            Console.WriteLine("*********BURASI SERVER*********");
            while (true)
            {

                Console.WriteLine("Waiting for a connection.");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client accepted.");
                NetworkStream stream = client.GetStream();

                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            recv++;
                        }
                    }

                    string response = sr.ReadLine();
                    if (response == "1")
                    {
                        backResponse = Convert.ToString(rastgele.Next());
                    }else if (response == "2")
                    {
                        char karakter = Convert.ToChar(rastgele.Next(65, 91));
                        //Console.WriteLine(karakter);
                        backResponse = Convert.ToString(karakter);
                    }
                    else
                    {
                        backResponse = "Yanlış Seçim";
                    }

                    Console.WriteLine(response);
                    Console.WriteLine("Serverda Üretilen Random Değer: " + backResponse);

                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine("request received");
                    //sw.WriteLine(response);
                    sw.WriteLine(backResponse);
                    sw.Flush();
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong.");
                    sw.WriteLine(e.ToString());
                }
            }
        }
    }
}
