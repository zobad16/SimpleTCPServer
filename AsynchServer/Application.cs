using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public class Application
    {
        string id;
        public Application(string _id, Parser parser)
        {
            id = _id;
            parser.RaiseTickEvent += OnTick;

        }

        private void OnTick(object sender, MarketData e)
        {
            Console.WriteLine("APP: {0} {1} {2} {3}", e.Source, e.Symbol, e.Bid, e.Ask);
        }
    }
}
