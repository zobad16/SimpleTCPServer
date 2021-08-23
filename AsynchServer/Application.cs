using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public class Application
    {
        string id;
        //private readonly MarketDataService;
        public Application(string _id)
        {
            id = _id;
            //parser.RaiseTickEvent += OnTick;
            //Ticker.Subscribe("XAUUSD.r", OnTick);

        }
        public void Initialize()
        {
            if (Ticker.Tick.ContainsKey("GC.Z21"))
                Ticker.Subscribe("GC.Z21", OnTick);
            if (Ticker.Tick.ContainsKey("GC-Z21"))
                Ticker.Subscribe("GC-Z21", OnTick);
            if (Ticker.Tick.ContainsKey("XAUUSD.r"))
                Ticker.Subscribe("XAUUSD.r", OnTick);
            
            
        }

        private void OnTick(object sender, MarketData e)
        {
            Console.WriteLine("APP: Source:{0} | Time: {6:MM/dd/yyy HH:mm:ss.fff} Symbol:{1} | Bid: {2} | Ask: {3} | High: {4} | Low: {5}", e.Source, e.Symbol, e.Bid, e.Ask, e.High, e.Low, e.Time);
        }
    }
}
