using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
   public static class Ticker
    {
        private static Dictionary<string, MarketData> _tick = new Dictionary<string, MarketData>();
        public static Dictionary<string, MarketData> Tick { get => _tick; set => _tick = value; }
        public static Dictionary<string, List<EventHandler<MarketData>>> Subscribers { get => _subscribers; set => _subscribers = value; }
        private static Dictionary<string, List<EventHandler<MarketData>>> _subscribers = new Dictionary<string, List<EventHandler<MarketData>>>();
        
        public static void Subscribe(string symbol, EventHandler<MarketData> eventHandler) {
            if (Subscribers.ContainsKey(symbol)) Subscribers[symbol].Add(eventHandler);
            else
            {
                var list = new List<EventHandler<MarketData>>();
                list.Add(eventHandler);
                Subscribers.Add(symbol, list);
            }
        }
        public static void Unsubscribe(string symbol, EventHandler<MarketData> eventHandler)
        {
            if (Subscribers.ContainsKey(symbol))
                Subscribers[symbol].Remove(eventHandler);
        }
        public static void InitializeTicker(Parser parser) {
            parser.RaiseTickEvent += TickHandler;
            Tick.Add("XAUUSD.r", new MarketData());
            Tick.Add("XAGUSD.r", new MarketData());
            Tick.Add("GC-Z21", new MarketData());
        }
        private static void TickHandler(object sender, MarketData e) {
            if (Tick.ContainsKey(e.Symbol))
                Tick[e.Symbol] = e;
            else
                Tick.Add(e.Symbol, e);
            //Invoke subscribers
            foreach(var handlers in Subscribers[e.Symbol])
            {
                handlers?.Invoke(e.Source, e);
            }

        }


    }
}
