using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4
{
    public interface IMediator
    {
        //void SendMessage(string message, User user);
        Task SendMessageAll(string message, User user);
        Task SendMessage(string message, User fromUer, User toUser);
    }
}
