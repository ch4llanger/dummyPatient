using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject
{
    public class Settings
    {
        public const string RabbitMQ = "RabbitMQ";
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionAddress { get; set; }
    }
}
