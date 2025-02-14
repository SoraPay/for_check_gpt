using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Seminar_4
{
    public class Message
    {
        public string? Text { get; set; }
        public DateTime? NowDate { get; set; }

        public Message(string text, DateTime dateTime)
        {
            Text = text;
            NowDate = dateTime;
        }
        public Message()
        {

        }
        public string SerializeJson() => JsonSerializer.Serialize(this);
        public static Message? DeserializeJson(string jsonDate) => JsonSerializer.Deserialize<Message>(jsonDate);
        public override string ToString()
        {
            return $" {NowDate}: {Text}";
        }
    }
}
