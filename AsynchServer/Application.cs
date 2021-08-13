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
            //parser.RaiseTickEvent += OnTick;
            //Ticker.Subscribe("XAUUSD.r", OnTick);

        }
        public void Initialize()
        {
            if (Ticker.Tick.ContainsKey("XAUUSD.r"))
                Ticker.Subscribe("XAUUSD.r", OnTick);
            if (Ticker.Tick.ContainsKey("GC.Z21"))
                Ticker.Subscribe("GC.Z21", OnTick);
        }

        private void OnTick(object sender, MarketData e)
        {
            Console.WriteLine("APP: {0} {1} {2} {3}", e.Source, e.Symbol, e.Bid, e.Ask);
        }
    }
}
