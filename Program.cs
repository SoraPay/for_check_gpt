//Вносим следующие изменения в код:

//1. Реализуем сервер как медиатор, позволяя ему отправлять сообщения конкретным клиентам по их имени.

//2. Добавляем функционал возврата списка всех подключенных к нему клиентов. 

//3. Расширьте функциональность сервера чата для массовой отправки сообщений и возможности определения всех подключенных клиентов.

//Добавьте поле ToName в класс сообщений.

using Seminar_4;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace MyNamespace
{
    class MyClass
    {

        //C:\Users\Сергей\Desktop\TempForLearn\Разработка_сетевого_приложения\Seminar_4\bin\Debug\net8.0\Seminar_4.exe Wolf
        static async Task Main(string[] args)
        {
            ChatMediator chatMediator = new ChatMediator();

            User mainServer = new User(
                name: "MainServer",
                mediator: chatMediator,
                udpClient: Server.udpClientServer,
                iPEndPoint: Server.iPEndPointServer
                );

            if(args.Length == 0)
            {
                Server server = new Server(user: mainServer, chatMediator);

                var ts1 = server.ReceiveMessage();

                var ts2 = server.SendMessage();

                await Task.WhenAll(ts1, ts2); 
            }
            else
            {
                User user1 = new User(
                name: $"{args[0]}",
                mediator: chatMediator,
                udpClient: Client.udpClientUser,
                iPEndPoint: Client.iPEndPointUser
                );

                Client client = new Client(user1);

                //var ts1 = client.ReceiveMessage();
                Console.WriteLine("Before start send");
                var ts2 = client.SendMessage(mainServer);

                await Task.WhenAll( ts2);

            }

        }
    }
}
