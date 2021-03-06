using AsynchServer.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AsynchServer
{
    public class Server
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private static Parser _parser;
        private static LoginService _loginService;
        /*public Server(Parser _parse, LoginService login)
        {
            _parser = _parse;
            _loginService = login;
        }*/
        public static void InitializeServer(Parser parse, LoginService login)
        {
            _parser = parse;
            _loginService = login;
        }
        public static void StartListening(int port = 9100) {
            //Establish local endpoint for socket
            IPHostEntry ipHostInfo      = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Any ;  
            IPEndPoint localEndPoint    = new IPEndPoint(ipAddress, port);

            //Create TCP/IP Socket
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try 
            {
                //Bind socket
                //Listen for incoming connection
                listener.Bind(localEndPoint);
                listener.Listen(120);
                while (true)
                {
                    //Set the event to nonsignaled
                    allDone.Reset();

                    //Start async socket to listen for connection
                    Console.WriteLine("Listening for connections on Port {0}", port);
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);
                    //Wait until a connection is made before continuing
                    allDone.WaitOne();
                }
            
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.Read();

        }
        public static void AcceptCallback(IAsyncResult ar) {
            //Signal main thread to continue
            try
            {
                allDone.Set();

                //Get the socket that handles client request
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                int _port = ((IPEndPoint)handler.RemoteEndPoint).Port;
                string ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
                //Create state object
                StateObject state = new StateObject();
                state.worksocket = handler;
                /*string lp_name = ConnectionManager.GetValue(ip, _port).LpName;
                ConnectionManager.AddSession(lp_name, handler);
                var session = ConnectionManager.Sessions[lp_name];
                */handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch(Exception e) {
                Console.WriteLine("Connection close. Reason {0}",e.Message);
            }
            
        }
        public static void ReadCallback(IAsyncResult ar) {

            try
            {

                String content = String.Empty;
                
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.worksocket;
                int _port = ((IPEndPoint)handler.RemoteEndPoint).Port;
                string ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
                string source = "";
                
                //Read data from client socket
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    //There might be more data, so store the data received so far
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //Check for end of file tag.
                    //If doesnt exist: read more data
                    content = state.sb.ToString();
                    
                    if (content.IndexOf("<EOF>") > -1 )
                    {
                        var lp = ConnectionManager.GetValue(ip, _port).LpName;
                        source =  lp;
                        if (!(content.IndexOf("q<EOF>") > -1))
                        {
                            //Console.WriteLine("Port{0}| Client Received: {1} -Read bytes {2}.\nData: {3}", _port, DateTime.Now.ToString("HH:mm:ss.ffffff"), content.Length, content);
                            _parser.ParseMessage(ar,source, content);
                            //Echo data back to the client
                            Send(handler, content);
                            state.sb.Clear();
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                        }
                        else
                        {
                            Console.WriteLine("Port{0}| Client Received: {1} -Read bytes {2}.\nData: {3}", _port, DateTime.Now.ToString("HH:mm:ss.ffffff"), content.Length, content);
                            Console.WriteLine("Data transmission stopped");
                            Console.WriteLine("======================================\n======================================");

                            handler.Close();
                            ConnectionManager.RemoveSession(source);
                            Ticker.RemoveTickerSource(source);
                        }
                    }
                    else if(!(content.IndexOf("<EOF>") > -1) )
                    {
                        //Not all data received. Get more
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReadCallback), state);
                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Read operation stopped. Connection close. Reason: {0}",e.Message);
            }
        }
        public static void Send(Socket handler, String data) {
            //Convert string to byte
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            //begin sending data 
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        public static void SendCallback(IAsyncResult ar) {
            try
            {
                //retrieve socket from state object
                Socket handler = (Socket)ar.AsyncState;

                //complete sending data to remote device
                int bytesSent = handler.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        
        }
    }
}
