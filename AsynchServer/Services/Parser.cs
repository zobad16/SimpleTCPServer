using AsynchServer.Model;
using AsynchServer.Services;
using AsynchServer.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsynchServer
{
    //TODO- Multiple type of Parser
    public class Parser
    {
        private readonly LoginService loginService;
        public Parser(LoginService login)
        {
            loginService = login;
        }
        public event EventHandler<Order> RaiseOrderExecutionEvent;
        public event EventHandler<MarketData> RaiseTickEvent;

        public void ParseMessage(IAsyncResult ar,string source, string message) {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.worksocket;
            int _port = ((IPEndPoint)handler.RemoteEndPoint).Port;
            string ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
            //From source select the type of parser to use
            //Check for initialization message
            if (source == "")
            {
                string initialization_msg = "</c>";
                if (message.IndexOf(initialization_msg) > -1)
                {
                    Console.WriteLine("Port{0}| Client Received: {1} -Read bytes {2}.\nData: {3}", _port, DateTime.Now.ToString("HH:mm:ss.ffffff"), message.Length, message);
                    loginService.CreateSession(ar, message);
                }

            }
            if (ConnectionManager.GetValue(source).Platform == "MT5")
                MessageParserMt5(source, message);
            
        }
        protected virtual void OnTick(MarketData tick)
        {
            Console.WriteLine("---- message received ----\nparsing message");
        }
        protected virtual void OnExecutionReport(MarketData tick)
        {
            Console.WriteLine("---- message received ----\nparsing message");
        }
        private void MessageParserMt5(string source, string message) {
            string[] mdl = message.Split("|");
            /*switch (mdl[0])
            {
                case AppProperties.ServerMessageType
                default:
                    break;
            }*/

            MarketData md = new MarketData();
            md.Time = Convert.ToDateTime(mdl[0]);
            md.Symbol = mdl[1];
            md.Bid = double.Parse(mdl[2]);
            md.Ask = double.Parse(mdl[3]);
            if (md.Ask > md.High) md.High = md.Ask;
            if (md.Bid < md.Low ) md.Low = md.Bid;
            if (md.Low == 0.00) md.Low = md.Bid;
            md.Source = source;
            
            RaiseTickEvent?.Invoke(this, md);
        }
        
    }
}
