using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public class MarketData
    {
        private string _source;
        private string _symbol;
        private double _bid;
        private double _ask;
        private double _open;
        private double _high;
        private double _low;
        private double _close;

        public MarketData(string source, string symbol, double bid, double ask, double open, double high, double low, double close)
        {
            _source = source;
            _symbol = symbol;
            _bid = bid;
            _ask = ask;
            _open = open;
            _high = high;
            _low = low;
            _close = close;
        }

        public string Source { get => _source; set => _source = value; }
        public string Symbol { get => _symbol; set => _symbol = value; }
        public double Bid { get => _bid; set => _bid = value; }
        public double Ask { get => _ask; set => _ask = value; }
        public double Open { get => _open; set => _open = value; }
        public double High { get => _high; set => _high = value; }
        public double Low { get => _low; set => _low = value; }
        public double Close { get => _close; set => _close = value; }
    }
}
