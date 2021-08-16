using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Client
{
    class Program
    {

        static void Main(string[] args)
        {

            Random rastgele = new Random();
            //int basamak=0;
            connection:
            try
            {
                while (true)
                {
                    TcpClient client = new TcpClient("127.0.0.1", 1302);
                    Console.WriteLine("*********BURASI CLİENT*********");

                    //string messageToSend = "Clientten Servera Mesaj..";
                    //string messageToSend = Convert.ToString(rastgele.Next());

                    Console.WriteLine("Hangi işlemi yapmak istiyorsunuz?\n\"1\"-Random Sayı\n\"2\"-Random Harf");
                    string messageToSend = Console.ReadLine();


                    int byteCount = Encoding.ASCII.GetByteCount(messageToSend + 1);
                    byte[] sendData = Encoding.ASCII.GetBytes(messageToSend);

                    NetworkStream stream = client.GetStream();
                    stream.Write(sendData, 0, sendData.Length);
                    Console.WriteLine("sending data to server...");

                    StreamWriter sw = new StreamWriter(stream);
                    sw.WriteLine(messageToSend);
                    sw.Flush();
                    StreamReader sr = new StreamReader(stream);
                    string response = sr.ReadLine();
                    Console.WriteLine(response);

                    stream.Close();
                    client.Close();
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("failed to connect...");
                Console.WriteLine(e);
                goto connection;
            }
        }
    }
}
