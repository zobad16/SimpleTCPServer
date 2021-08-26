using AsynchServer.Services;
using System;
using System.Threading;

namespace AsynchServer
{
    class Program
    {
        static int Main(string[] args)
        {
            //initialize services
            LoginService login = new LoginService();
            MarketDataService md = new MarketDataService();
            OrderService orderService = new OrderService();
            Parser parser = new Parser(login, orderService);
            Ticker.InitializeHandler(parser);
            Application mt5app = new Application("MT5 APP",md,orderService);
            mt5app.Initialize();
            Server.InitializeServer( parser, login);
            ConnectionManager.Add("Online Fintech I",new LiquidityProvider("Online Fintech","MT5", "987607", "127.0.0.1",91));
            ConnectionManager.Add("VPFX", new LiquidityProvider("VPFX","MT5", "987416", "127.0.0.1", 92));
            Console.WriteLine("Starting server");
            Server.StartListening(91);
            
            return 0;
        }
    }
}
