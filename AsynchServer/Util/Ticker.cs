using AsynchServer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
   public static class Ticker
    {
        private static Dictionary<TickerKey, MarketData> _tick = new Dictionary<TickerKey, MarketData>();
        public static Dictionary<TickerKey, MarketData> Tick { get => _tick; set => _tick = value; }
        public static Dictionary<string, List<EventHandler<MarketData>>> Subscribers { get => _subscribers; set => _subscribers = value; }
        private static Dictionary<string, List<EventHandler<MarketData>>> _subscribers = new Dictionary<string, List<EventHandler<MarketData>>>();
        
        public static void Subscribe(string symbol, EventHandler<MarketData> eventHandler) {
            if (Subscribers.ContainsKey(symbol)) Subscribers[symbol].Add(eventHandler);
            else
            {
                Subscribers.Add(symbol, new List<EventHandler<MarketData>>());
                Subscribers[symbol].Add(eventHandler);
            }
        }
        public static void Unsubscribe(string symbol, EventHandler<MarketData> eventHandler)
        {
            if (Subscribers.ContainsKey(symbol))
                Subscribers[symbol].Remove(eventHandler);
        }
        public static void InitializeTicker(string source,Parser parser) {
            parser.RaiseTickEvent += TickHandler;
            var key = new TickerKey(source, "");

            Tick.Add(new TickerKey(source, "XAUUSD.r"), new MarketData());
            Tick.Add(new TickerKey(source, "XAGUSD.r"), new MarketData());
            Tick.Add(new TickerKey(source, "GC-Z21"), new MarketData());
            Tick.Add(new TickerKey(source, "GC.Z21"), new MarketData());
        }
        private static void TickHandler(object sender, MarketData e) {
            var key = new TickerKey(e.Source, e.Symbol);
            if (Tick.ContainsKey(key))
            {
                Tick[key].Bid = e.Bid;
                Tick[key].Ask = e.Ask;
                if(Tick[key].High < e.Ask)
                    Tick[key].High = e.Ask;
                if (Tick[key].Low > 0.0 &&
                    Tick[key].Low > e.Bid)
                    Tick[key].Low = e.Bid;
            }
                
            else
            {
                Tick.Add(key, e);
                Subscribers.TryAdd(e.Symbol, new List<EventHandler<MarketData>>());

            }
                
            //Invoke subscribers
            foreach(var handlers in Subscribers[e.Symbol])
            {
                handlers?.Invoke(e.Source, e);
            }

        }


    }
}
