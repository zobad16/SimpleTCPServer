using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public class Parser
    {
        public Parser()
        {

        }
        public event EventHandler<MarketData> RaiseTickEvent;
        public void Parse(string source, string message) {
            RaiseTickEvent?.Invoke(this, new MarketData(source, "XAUUSD.r", 1737.03, 1737.10, 1737.03, 1740.00, 1737.00, 1737.02));
        }
        protected virtual void OnTick(MarketData tick)
        {
            Console.WriteLine("---- message received ----\nparsing message");
        }
        
    }
}
