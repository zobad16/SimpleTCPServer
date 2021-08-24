using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.Services
{
    public class MarketDataService
    {
        public MarketDataService()
        {

        }

        public void Subscribe(string symbol, EventHandler<MarketData> eventHandler) {
            Ticker.Subscribe(symbol, eventHandler);
        }
        public void Unsubscribe(string symbol, EventHandler<MarketData> eventHandler) {
            Ticker.Unsubscribe(symbol, eventHandler);
        }
        /*public void UnsubscribeBySource(string source, EventHandler<MarketData> eventHandler)
        {
            foreach(var item in Ticker.Subscribers)
            {
                
            }
            Ticker.Subscribers
        }*/
        public void Initialize(List<string> list,EventHandler<MarketData> eventHandler)
        {
            foreach(var l in list)
            {
                Subscribe(l, eventHandler);
            }
        }
    }
}
