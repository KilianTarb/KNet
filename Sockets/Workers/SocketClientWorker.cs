using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using kNet.Sockets.SocketEventArgs;
using System.Threading;

namespace kNet.Sockets.Workers
{
    public class SocketClientWorker
    {
        #region Member Variables
        /// <summary>
        /// The underlying socket.
        /// </summary>
        public EventSocket Client { get; private set; }

        /// <summary>
        /// The remote endpoint that the socket is connected to.
        /// </summary>
        public EndPoint RemoteEndPoint { get; private set; }

        /// <summary>
        /// The thread that the socket is running on.
        /// </summary>
        public Thread SocketThread { get; private set; }

        /// <summary>
        /// When data is received to the client.
        /// </summary>
        public event EventHandler<SocketReceiveEventArgs> ConnectionRecieve;

        /// <summary>
        /// When data is sent to the connected listener.
        /// </summary>
        public event EventHandler<SocketReceiveEventArgs> ConnectionSend;

        /// <summary>
        /// Client is in the process of connecting.
        /// </summary>
        public event EventHandler<SocketConnectEventArgs> Connecting;

        /// <summary>
        /// If socket throws an error.
        /// </summary>
        public event EventHandler<SocketErrorEventArgs> Error;

        /// <summary>
        /// Cliet has successfully connected.
        /// </summary>
        public event EventHandler<SocketConnectEventArgs> Connected;

        private ManualResetEvent _allDone = new ManualResetEvent(false);
        private BackgroundWorker _worker = new BackgroundWorker();
        #endregion


        #region Constuctors
        public SocketClientWorker()
        {
            // Init socket
            Client = new EventSocket(SocketType.Stream, ProtocolType.Tcp);
            SocketThread = Thread.CurrentThread;

            // Sub events
            _worker.DoWork += _worker_DoWork;
        }
        #endregion


        #region Event Definitions
        // When data is recieved to the socket.
        protected virtual void OnConnectionRecieve(State state)
        {
            ConnectionRecieve?.Invoke(this, new SocketReceiveEventArgs() { ReceiveState = state });
        }
        // When data is sent to the conncted host.
        protected virtual void OnConnectionSend(State state)
        {
            ConnectionSend?.Invoke(this, new SocketReceiveEventArgs() { ReceiveState = state });
        }
        // When the socket is connecting to the host.
        protected virtual void OnConnecting(EndPoint remoteEndPoint)
        {
            Connecting?.Invoke(this, new SocketConnectEventArgs() { RemoteEndPoint = remoteEndPoint });
        }
        // When the socket is connected to the host.
        protected virtual void OnConnected(EndPoint remoteEndPoint)
        {
            Connected?.Invoke(this, new SocketConnectEventArgs() { RemoteEndPoint = remoteEndPoint });
        }
        // When the socket throw an error.
        protected virtual void OnError(SocketException exception)
        {
            Error?.Invoke(this, new SocketErrorEventArgs(exception));
        }
        #endregion


        #region Event Subscriptions
        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Connect to host, also raise events.
                OnConnecting(RemoteEndPoint);
                Client.Connect(RemoteEndPoint);
                OnConnected(RemoteEndPoint);

                // Begin the receiving cycle.
                State state = new State();
                _allDone.Set();
                Client.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
                _allDone.WaitOne();
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }
        #endregion


        #region Member Methods
        /// <summary>
        /// Start the worker.
        /// </summary>
        /// <param name="remoteEndPoint"></param>
        public void Start(EndPoint remoteEndPoint)
        {
            // Set the public endpoint.
            RemoteEndPoint = remoteEndPoint;

            // Start the worker.
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Sends an encoded message to the listener socket.
        /// </summary>
        /// <param name="message">Encoded message</param>
        public void Send(byte[] message)
        {
            try
            {
                // Begin send to host.
                State state = new State() { buffer = message, socket = Client };
                Client.BeginSend(state.buffer, 0, state.buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), Client);
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                // Store values from the IAsyncResult.
                string content = string.Empty;
                State state = (State)result.AsyncState;
                Socket handler = state.socket;

                // Read the data bytes.
                int bytesRead = Client.EndReceive(result);

                if (bytesRead > 0)
                {
                    // Add to the string builder and raise the Data Recieve event.
                    state.stringBuilder.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    OnConnectionRecieve(state);
                    state.stringBuilder.Clear();
                }
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }

        private void SendCallback(IAsyncResult result)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)result.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(result);
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }
        #endregion
    }
}
