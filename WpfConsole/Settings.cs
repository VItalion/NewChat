using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfConsole

{
    [Serializable]
    public class Settings
    {
        public string NetProtocol { get; set; }

        public string IpServer { get; set; }

        public int Port { get; set; }

        public Uri Uri { get; set; }

        public void CreateUri()
        {
            //Random port = new Random();
            //Port = port.Next(1234, 49000);
            Uri = new Uri(NetProtocol + "://" + IpServer + '/' + Port + '/' + "Chat");
        }

    }
}
