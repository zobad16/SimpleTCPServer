using System;
using System.Threading;

namespace AsynchServer
{
    class Program
    {
        static int Main(string[] args)
        {
            Parser parser = new Parser();
            Application app = new Application("Main APP",parser);
            Server.Parser = parser;
            ConnectionManager.Add("Online Fintech I",new LiquidityProvider("Online Fintech", "987607", "127.0.0.1",91));
            ConnectionManager.Add("VPFX", new LiquidityProvider("VPFX", "987416", "127.0.0.1", 92));
            Console.WriteLine("Starting server");
            foreach(var lp in ConnectionManager.Lp)
            {
                
                new Thread(() => Server.StartListening(lp.Value.Port)).Start();
            }
            return 0;
        }
    }
}
