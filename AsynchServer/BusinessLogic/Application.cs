using AsynchServer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public class Application:IDisposable
    {
        string id;
        private readonly MarketDataService mdService;
        private readonly OrderService orderService;
        public Application(string _id, MarketDataService md, OrderService os)
        {
            id = _id;
            mdService = md;
            orderService = os;
        }
        public void Initialize()
        {
            var mdList = new List<string>();
            mdList.Add("GC.Z21");
            mdList.Add("GC-Z21");
            mdList.Add("XAUUSD.r");
            mdService.Initialize(mdList,OnTick);
            orderService.Initialize();
        }
        public void DeInitilize()
        {
            mdService.Unsubscribe("GC.Z21", OnTick);
            mdService.Unsubscribe("GC-Z21", OnTick);
            mdService.Unsubscribe("XAUUSD.r", OnTick);
        }

        private void OnTick(object sender, MarketData e)
        {
            Console.WriteLine("APP: Source:{0} | Time: {1:MM/dd/yyy HH:mm:ss.fff} Symbol:{2} | Bid: {3} | Ask: {4} | High: {5} | Low: {6}", e.Source, e.Time, e.Symbol, e.Bid, e.Ask, e.High, e.Low );
        }

        public void Dispose()
        {
            DeInitilize();
            
        }
    }
}
