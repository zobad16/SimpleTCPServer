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
        private readonly OrderService orderService;
        public Parser(LoginService login, OrderService ods)
        {
            loginService = login;
            orderService = ods;
        }
        public event EventHandler<Order> RaiseOrderExecutionEvent;
        public event EventHandler<MarketData> RaiseTickEvent;
        public bool IsMDEventHandlerRegistered(Delegate prospectiveHandler)
        {
            if (this.RaiseTickEvent != null)
            {
                foreach (Delegate existingHandler in this.RaiseTickEvent.GetInvocationList())
                {
                    if (existingHandler == prospectiveHandler)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsOrderEventHandlerRegistered(Delegate prospectiveHandler)
        {
            if (this.RaiseOrderExecutionEvent != null)
            {
                foreach (Delegate existingHandler in this.RaiseOrderExecutionEvent.GetInvocationList())
                {
                    if (existingHandler == prospectiveHandler)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void ParseMessage(IAsyncResult ar,string source, string message) {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.worksocket;
            int _port = ((IPEndPoint)handler.RemoteEndPoint).Port;
            string ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
            //From source select the type of parser to use
            //Check for initialization message
            string[] msg = message.Split("|");
            int msg_type = (int)AppProperties.MessageField.MESSAGE_TYPE;
            int msg_source = (int)AppProperties.MessageField.CLIENT_ID;
            int msg_data = (int)AppProperties.MessageField.DATA;
            if (source == "")
            {
                
                string msg_type_login = ""+(int)AppProperties.MessageType.LOGIN;
                
                if (msg[msg_type].Equals(msg_type_login)
                    && msg[msg_data].Equals("Request"))
                {
                    Ticker.InitializeTicker(msg[msg_source],this);
                    Console.WriteLine("Port{0}| Client Received: {1} -Read bytes {2}.\nData: {3}", _port, DateTime.Now.ToString("HH:mm:ss.ffffff"), message.Length, message);
                    loginService.CreateSession(ar, msg[msg_source]);
                }
                              
            }
            else
            {
                string md = ""+AppProperties.GetIntValue(AppProperties.MessageType.MARKET_DATA);
                if (msg[msg_type].Equals(md))
                {
                    MessageParserMt5(msg[msg_source], msg[AppProperties.GetIntValue(AppProperties.MessageField.DATE)], msg[AppProperties.GetIntValue(AppProperties.MessageField.DATA)]);
                }
            }
            //if (ConnectionManager.GetValue(source).Platform == "MT5")
               // MessageParserMt5(source, message);
            
        }
        protected virtual void OnTick(MarketData tick)
        {
            Console.WriteLine("---- message received ----\nparsing message");
        }
        protected virtual void OnExecutionReport(MarketData tick)
        {
            Console.WriteLine("---- message received ----\nparsing message");
        }
        private void MessageParserMt5(string source,string date, string message) {
            string[] mdl = message.Split(";");
            /*switch (mdl[0])
            {
                case AppProperties.ServerMessageType
                default:
                    break;
            }*/
             
            MarketData md = new MarketData();
            md.Time = Convert.ToDateTime(date);
            md.Symbol = mdl[0];
            md.Bid = double.Parse(mdl[1]);
            md.Ask = double.Parse(mdl[2]);
            if (md.Ask > md.High) md.High = md.Ask;
            if (md.Bid < md.Low ) md.Low = md.Bid;
            if (md.Low == 0.00) md.Low = md.Bid;
            md.Source = source;
            
            RaiseTickEvent?.Invoke(this, md);
        }
        
    }
}
