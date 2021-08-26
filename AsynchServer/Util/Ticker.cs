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
        public static void InitializeHandler(Parser parser)
        {
            try
            {
                parser.RaiseTickEvent -= TickHandler;
                parser.RaiseTickEvent += TickHandler;
            }
            catch(Exception e)
            {
                parser.RaiseTickEvent += TickHandler;
            }
            
        }
        public static void InitializeTicker(string source,Parser parser) {
            var key = new TickerKey(source, "");
            if(!Tick.ContainsKey(new TickerKey(source, "XAUUSD.r")))
                Tick.Add(new TickerKey(source, "XAUUSD.r"), new MarketData());
            if (!Tick.ContainsKey(new TickerKey(source, "XAGUSD.r")))
                Tick.Add(new TickerKey(source, "XAGUSD.r"), new MarketData());
            if (!Tick.ContainsKey(new TickerKey(source, "GC-Z21"))) 
                Tick.Add(new TickerKey(source, "GC-Z21"), new MarketData());
            if (!Tick.ContainsKey(new TickerKey(source, "GC.Z21"))) 
                Tick.Add(new TickerKey(source, "GC.Z21"), new MarketData());
        }
        public static void RemoveTickerSource(string source)
        {
            foreach(var item in Tick)
            {
                if (item.Key.Source == source)
                    Tick.Remove(item.Key);
            }
        }
        private static void TickHandler(object sender, MarketData e) {
            TickerKey key = new TickerKey(e.Source, e.Symbol);
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
        private static bool IsExist(TickerKey key)
        {
            try
            {
                foreach (var item in Tick)
                {
                    if (item.Key.Equals(key))
                        return true;
                }
            }
            catch(Exception e)
            {

                return false;
            }
            return false;
        }


    }
}
