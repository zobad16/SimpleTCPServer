using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace AsynchServer
{
    public static class ConnectionManager
    {
        private static Dictionary<string, LiquidityProvider> _Lp= new Dictionary<string, LiquidityProvider>();
        private static Dictionary<string, Socket> _sessions = new Dictionary<string, Socket>();
        
        public static Dictionary<string, LiquidityProvider> Lp { get => _Lp; set => _Lp = value; }
        public static Dictionary<string, Socket> Sessions { get => _sessions; set => _sessions = value; }

        public static void AddSession(string name, Socket handle)
        {
            if (!IsSessionExists(name))
                Sessions.Add(name, handle);
            else
                EditSession(name, handle);
        }
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
        public static void EditSession(string name, Socket handle)
        {
            bool isExists = IsSessionExists(name);
            if (isExists) Sessions[name] = handle;
        }
        public static void Remove(string name)
        {
            Lp.Remove(name);
        }
        public static void RemoveSession(string name)
        {
            Sessions.Remove(name);
        }
        public static bool IsExists(string name)
        {            
            return Lp.ContainsKey(name);
        }
        public static bool IsSessionExists(string name)
        {
            return Sessions.ContainsKey(name);            
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
