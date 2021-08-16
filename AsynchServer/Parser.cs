using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    //TODO- Multiple type of Parser
    public class Parser
    {
        public Parser()
        {

        }
        public event EventHandler<MarketData> RaiseTickEvent;
        public void Parse(string source, string message) {
            //From source select the type of parser to use
            if (ConnectionManager.GetValue(source).Platform == "MT5")
                MessageParserMt5(source, message);
            
        }
        protected virtual void OnTick(MarketData tick)
        {
            Console.WriteLine("---- message received ----\nparsing message");
        }
        private void MessageParserMt5(string source, string message) {
            MarketData md = new MarketData();
            string[] mdl = message.Split("|");
            md.Time = Convert.ToDateTime(mdl[0]);
            md.Symbol = mdl[1];
            md.Bid = double.Parse(mdl[2]);
            md.Ask = double.Parse(mdl[3]);
            if (md.Ask > md.High) md.High = md.Ask;
            if (md.Bid < md.Low ) md.Low = md.Bid;
            if (md.Low == 0.00) md.Low = md.Bid;
            md.Source = source;
            
            RaiseTickEvent?.Invoke(this, md);
        }
        
    }
}
