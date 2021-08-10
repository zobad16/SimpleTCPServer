using System;
using System.Threading;

namespace AsynchServer
{
    class Program
    {
        static int Main(string[] args)
        {
            ConnectionManager manager = new ConnectionManager();
            manager.Add("Online Fintech I",new LiquidityProvider("Online Fintech", "987607", "127.0.0.1",91));
            manager.Add("VPFX", new LiquidityProvider("VPFX", "987416", "127.0.0.1", 92));
            Console.WriteLine("Starting server");
            foreach(var lp in manager.Lp)
            {
                
                new Thread(() => Server.StartListening(lp.Value.Port)).Start();
            }
            return 0;
        }
    }
}
