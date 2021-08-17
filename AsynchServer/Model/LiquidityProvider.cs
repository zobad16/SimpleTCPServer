using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
   public class LiquidityProvider
    {
        private string _lpName;
        private string _platform;
        private string _account;
        private string _ip;
        private int _port;

        public LiquidityProvider()
        {
            _lpName = "";
            _platform = "";
            _account = "";
            _ip = "0.0.0.0";
            _port = 0;

        }

        public LiquidityProvider(string lpName, string platform, string account ,string ip, int port)
        {
            _lpName = lpName;
            _platform = platform;
            _account = account;
            _ip = ip;
            _port = port;
        }

        public int Port { get => _port; set => _port = value; }
        public string Ip { get => _ip; set => _ip = value; }
        public string LpName { get => _lpName; set => _lpName = value; }
        public string Account { get => _account; set => _account = value; }
        public string Platform { get => _platform; set => _platform = value; }
    }
}
