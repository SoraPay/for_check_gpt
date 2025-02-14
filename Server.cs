using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4
{
    internal class Server
    {

        public static UdpClient udpClientServer = new UdpClient(12345);

        public static IPEndPoint iPEndPointServer = new IPEndPoint(IPAddress.Any, 12345);

        //User user = new User("Server_main", chatMediator, true, 12345);
        //chatMediator.Registr(user);
        private User PersonServer { get; }

        //private IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 12345);
        //private UdpClient udpClient = new UdpClient(12345);
        public Server(User user, ChatMediator chatMediator)
        {
            PersonServer = user;
            chatMediator.Registr(user);
        }
        public async Task ReceiveMessage()

        {
            while (true)
            {
                Console.WriteLine("Server started...");

                while (true)
                {
                    try
                    {
                        // Получаем данные от клиента
                        UdpReceiveResult result = await PersonServer.UdpClient.ReceiveAsync();
                        Console.WriteLine("Server received data");

                        byte[] buffer = result.Buffer;
                        string str = Encoding.UTF8.GetString(buffer);

                        User? person = User.DeserializeJson(str);



                        if (person != null)
                        {
                            string? textPerson = person?.message?.Text;
                            Console.WriteLine($"Received message: {textPerson}");

                            if (!ChatMediator.connectNowUsers.ContainsKey(person))
                                ChatMediator.connectNowUsers.Add(person,result.RemoteEndPoint);

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
        }
        public async Task SendMessage()
        {

            Console.WriteLine("Server to the ready send message...");

            ChatMediator chatMediator = new ChatMediator();

            while (true)
            {
                try
                {
                    string? strText = Console.ReadLine();
                    if(!string.IsNullOrEmpty(strText))
                    {
                        await chatMediator.SendMessageAll(strText, PersonServer);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Server error: {ex.Message}");
                }
            }


        }
    }
}
