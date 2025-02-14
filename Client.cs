using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4
{
    internal class Client
    {

        public static UdpClient udpClientUser = new UdpClient();


        public static IPEndPoint iPEndPointUser = new IPEndPoint(IPAddress.Parse(local), 12345);

        private User User { get; }

        public Client(User user)
        {
            User = user;
        }
        const string local = "127.0.0.1";
        //private IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(local), 12345);
        //private UdpClient udpClient = new UdpClient();

        public async Task ReceiveMessage()
        {
            Console.WriteLine("You listen chat, " + User.Name);
            User.UdpClient.Connect(local, 12345);

            while (true)
            {
                try
                {
                    // Получаем данные от клиента
                    Console.WriteLine("Try start ReceiveAsync");
                    UdpReceiveResult result = await User.UdpClient.ReceiveAsync();
                    Console.WriteLine("Server received data");

                    byte[] buffer = result.Buffer;
                    string str = Encoding.UTF8.GetString(buffer);

                    User? person = User.DeserializeJson(str);

                    if (person != null)
                    {
                        string? textPerson = person?.message?.Text;
                        Console.WriteLine($"Received message: {textPerson}");
                    }
                    else
                        Console.WriteLine("Person is empty");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Server error: {ex.Message}");
                }
            }



        }
        public async Task SendMessage(User userServer)
        {

            Console.WriteLine("You can to write message, " + User.Name);
            User.UdpClient.Connect(local, 12345);

            while (true)
            {
                User userNow = User;

                //await Task.Run(async () =>
                //{
                //    string? strEnter = Console.ReadLine();
                //    await userNow.SendMessage(strEnter ?? "null", userServer);

                //});

                try
                {
                    string? strEnter = Console.ReadLine();
                    await userNow.SendMessage(strEnter ?? "null", userServer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Server error: {ex.Message}");
                }

                //if (Console.ReadKey().Key == ConsoleKey.Escape)
                //{
                //    Console.WriteLine("Goodby");
                //    break;
                //}
            }

        }

    }
}
