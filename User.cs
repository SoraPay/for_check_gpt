using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Seminar_4
{
    public class User
    {
        public Message? message;

        private IMediator mediator;

        public UdpClient UdpClient { get; }

        public IPEndPoint IPEndPoint { get; }
        public string Name { get; }
        public bool IsConnected { get; set; }
        public int NowPort { get; set; }

        public User(string name, IMediator mediator, UdpClient udpClient, IPEndPoint iPEndPoint)
        {
            Name = name;
            this.mediator = mediator;
            UdpClient = udpClient;
            this.IPEndPoint = iPEndPoint;
        }

        public async Task SendMessage(string message,User ToSend)
        {
           await mediator.SendMessage(message,this, ToSend);
        }
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"{Name} to receive {message}");
        }
        public string SerializeJson() => JsonSerializer.Serialize(this);
        public static User? DeserializeJson(string jsonDate) => JsonSerializer.Deserialize<User>(jsonDate);


    }
}
