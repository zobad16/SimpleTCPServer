using System;
using System.Collections.Generic;
using System.Text;

namespace AsynchServer
{
    public static class ConnectionManager
    {
        private static Dictionary<string, LiquidityProvider> _Lp= new Dictionary<string, LiquidityProvider>();
        
        
        public static Dictionary<string, LiquidityProvider> Lp { get => _Lp; set => _Lp = value; }
        
        public static void Add(string name, LiquidityProvider lp)
        {
            bool isExists = IsExists(name);
            if (!isExists) Lp.Add(name, lp);
            else Edit(name, lp);
        }
        public static void Edit(string name, LiquidityProvider lp)
        {
            bool isExists = IsExists(name);
            if (isExists) Lp[name] = lp;
        }
        public static void Remove(string name)
        {
            Lp.Remove(name);
        }
        public static bool IsExists(string name)
        {
            Lp.ContainsKey(name);
            return false;
        }
        public static LiquidityProvider GetValue(string name)
        {
            return Lp[name];
        }
        public static LiquidityProvider GetValue(string ip, int port)
        { 
            foreach(var con in Lp)
            {
                if (con.Value.Ip == ip && con.Value.Port == port)
                    return con.Value;
            }
            return null;
        }

    }
}
