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
            int pos = message.IndexOf('<');
            string parsed_string = message.Remove(pos);
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.worksocket;
            int _port = ((IPEndPoint)handler.RemoteEndPoint).Port;
            string ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
            ConnectionManager.Add(parsed_string, new LiquidityProvider(parsed_string, "MT5", "", ip, _port));
            ConnectionManager.AddSession(parsed_string, handler);
            Console.WriteLine("Client {0} initialized. Session created", parsed_string);
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

            Server.Send(handler, "<ack>");
        }
    }
}
