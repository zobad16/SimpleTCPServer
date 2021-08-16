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
            //Instead of defining here.
            //We just listen on 1 port
            //When the connection accepts
            //First message sends LP name
            //We receive that name and add it ConnectionManager and session manager
            //We store remote port for each Lp
            ConnectionManager.Add("Online Fintech I",new LiquidityProvider("Online Fintech","MT5", "987607", "127.0.0.1",91));
            ConnectionManager.Add("VPFX", new LiquidityProvider("VPFX","MT5", "987416", "127.0.0.1", 92));
            Console.WriteLine("Starting server");
            Server.StartListening(91);
            /*foreach (var lp in ConnectionManager.Lp)
            {
                
                new Thread(() => Server.StartListening(lp.Value.Port)).Start();
            }*/
            return 0;
        }
    }
}
