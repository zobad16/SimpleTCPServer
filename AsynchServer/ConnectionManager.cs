using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public class ConnectionManager
    {
        Dictionary<string, LiquidityProvider> _Lp= new Dictionary<string, LiquidityProvider>();

        public Dictionary<string, LiquidityProvider> Lp { get => _Lp; set => _Lp = value; }

        public void Add(string name, LiquidityProvider lp)
        {
            bool isExists = IsExists(name);
            if (!isExists) Lp.Add(name, lp);
            else Edit(name, lp);
        }
        public void Edit(string name, LiquidityProvider lp)
        {
            bool isExists = IsExists(name);
            if (isExists) Lp[name] = lp;
        }
        public void Remove(string name)
        {
            Lp.Remove(name);
        }
        public bool IsExists(string name)
        {
            Lp.ContainsKey(name);
            return false;
        }
        public LiquidityProvider GetValue(string name)
        {
            return Lp[name];
        }

    }
}
