using System;
using System.Threading;

namespace AsynchServer
{
    class Program
    {
        static int Main(string[] args)
        {
            Parser parser = new Parser();
            Ticker.InitializeTicker(parser);
            Application mt5app = new Application("MT5 APP");
            mt5app.Initialize();
            Server.Parser = parser;
            ConnectionManager.Add("Online Fintech I",new LiquidityProvider("Online Fintech","MT5", "987607", "127.0.0.1",91));
            ConnectionManager.Add("VPFX", new LiquidityProvider("VPFX","MT5", "987416", "127.0.0.1", 92));
            Console.WriteLine("Starting server");
            Server.StartListening(91);
            
            return 0;
        }
    }
}
