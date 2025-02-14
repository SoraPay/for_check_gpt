using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4
{
    internal class ChatMediator : IMediator
    {
        private List<User> users = new List<User>();

        public static Dictionary<User, IPEndPoint> connectNowUsers = new Dictionary<User, IPEndPoint>();
        public void Registr(User user)
        {
            users.Add(user);
        }
        //public void SendMessage(string message, User user)
        //{
        //    foreach (var item in users)
        //    {
        //        if (user != item)
        //            Console.WriteLine(message);
        //    }
        //}

        public async Task SendMessageAll(string message, User FromServer)
        {
            if (connectNowUsers.Count == 0)
            {
                Console.WriteLine("Connection users is not");
                return;
            }


            List<Task> sendTasks = new List<Task>();

            foreach (var item in connectNowUsers)
            {
                string personMessage = $"Dir {item.Key.Name}. {message}";

                Message responseMessage = new Message(personMessage, DateTime.Now);
                FromServer.message = responseMessage;

                string strJson = FromServer.SerializeJson();
                byte[] responseBytes = Encoding.UTF8.GetBytes(strJson);

                sendTasks.Add(FromServer.UdpClient.SendAsync(responseBytes,responseBytes.Length,item.Value));

            }

            await Task.WhenAll(sendTasks);

        }

        public async Task SendMessage(string message, User FromUser,User ToUser)
        {
            Message messages = new Message(message, DateTime.Now);
            FromUser.message = messages;

            string strJson = FromUser.SerializeJson();
            byte[] buffer = Encoding.UTF8.GetBytes(strJson);

            await FromUser.UdpClient.SendAsync(buffer, buffer.Length, ToUser.IPEndPoint);
        }
    }
}
