using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer.Model
{
    public class TickerKey
    {
        public TickerKey()
        {
            Source = "";
            Symbol = "";
        }
        private string _source;
        private string _symbol;

        public TickerKey(string source, string symbol)
        {
            Source = source;
            Symbol = symbol;
        }

        public string Source { get => _source; set => _source = value; }
        public string Symbol { get => _symbol; set => _symbol = value; }

        public override bool Equals(object obj)
        {
            return obj is TickerKey key &&
                   Source == key.Source &&
                   Symbol == key.Symbol;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_source, _symbol);
        }
    }
}
