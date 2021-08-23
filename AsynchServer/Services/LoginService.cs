using AsynchServer.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsynchServer.Services
{
    public class LoginService
    {
        public LoginService()
        {

        }
        public void CreateSession(IAsyncResult ar, string message)
        {
            //Create an entry in lp and sessions
            //in lp for ip and port set the destination ip and port
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.worksocket;
            int _port = ((IPEndPoint)handler.RemoteEndPoint).Port;
            string ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
            ConnectionManager.Add(message, new LiquidityProvider(message, "MT5", "", ip, _port));
            ConnectionManager.AddSession(message, handler);
            Console.WriteLine("Client {0} initialized. Session created", message);
            Console.WriteLine("======================================\nStarting Market Data\n======================================");
            state.sb.Clear();
            SendAck(ar);

            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(Server.ReadCallback), state);
        }
        private void SendAck(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.worksocket;
            string ack = (int)AppProperties.MessageType.LOGIN + "|SERVER|" + DateTime.Now + "|Accept|<EOF>";
            Server.Send(handler, ack);
        }
    }
}
