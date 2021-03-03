using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Threading;
using kNet.Sockets.SocketEventArgs;

namespace kNet.Sockets.Workers
{
    public class SocketListenerWorker
    {
        #region Member variables
        /// <summary>
        /// When the socket has started listening for new connections.
        /// </summary>
        public event EventHandler<SocketListenEventArgs> Started;

        /// <summary>
        /// When the socket has stopped.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// When the socket has thrown an error.
        /// </summary>
        public event EventHandler<SocketErrorEventArgs> Error;

        /// <summary>
        /// When a new connection is established.
        /// </summary>
        public event EventHandler<SocketNewConnectionEventArgs> NewConnection;

        /// <summary>
        /// When the socket recieves data.
        /// </summary>
        public event EventHandler<SocketReceiveEventArgs> ConnectionRecieve;

        /// <summary>
        /// The thread where the socket is running.
        /// </summary>
        public Thread SocketThread { get; private set; }

        /// <summary>
        /// Handles all the connections.
        /// </summary>
        public ConnectionManager Manager { get; private set; }

        // Main worker.
        private BackgroundWorker _worker = new BackgroundWorker();

        // The socket that will be used to listen.
        private EventSocket _mainSocket;

        // Thread signal.
        private static ManualResetEvent _allDone = new ManualResetEvent(false);

        // The local endpoint that the socket will listen to.
        private readonly IPEndPoint _endpoint;
        #endregion

        #region Constructors
        public SocketListenerWorker(IPEndPoint localendpoint)
        {
            // Set properties
            Manager = new ConnectionManager(_mainSocket);
            _endpoint = localendpoint;


            // Subscribe worker events
            _worker.DoWork += _worker_DoWork;
        }
        #endregion

        #region Worker Events
        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SocketThread = Thread.CurrentThread;
            SocketThread.Name = "SOCKET_LISTENER_THREAD";

            StartAcceptingConnections();
        }
        #endregion

        #region Event Definitions
        protected virtual void OnStarted(int backlog, EndPoint localendpoint)
        {
            Started?.Invoke(this, new SocketListenEventArgs() { Backlog = backlog, LocalEndPoint = localendpoint});
        }

        protected virtual void OnStopped()
        {
            Stopped?.Invoke(this, new EventArgs());
        }

        protected virtual void OnError(SocketException ex)
        {
            Error?.Invoke(this, new SocketErrorEventArgs(ex));
        }

        protected virtual void OnNewConnection(Socket handler)
        {
            NewConnection?.Invoke(this, new SocketNewConnectionEventArgs(handler));
        }

        protected virtual void OnConnectionRecieve(State state)
        {
            ConnectionRecieve?.Invoke(this, new SocketReceiveEventArgs() { ReceiveState = state });
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the listening process.
        /// </summary>
        public void Start()
        {
            try
            {
                _mainSocket = new EventSocket(SocketType.Stream, ProtocolType.Tcp);

                int backlog = 10;

                // Set the socket to listen to the local endpoint.
                _mainSocket.Bind(_endpoint);
                _mainSocket.Listen(backlog);
                OnStarted(backlog, _endpoint);
                _worker.RunWorkerAsync();
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }

        /// <summary>
        /// Stops the listening process and connections.
        /// </summary>
        public void Stop()
        {
            try
            {
                _mainSocket.Close();
                OnStopped();
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }

        /// <summary>
        /// Start actively accepting connections.
        /// </summary>
        private void StartAcceptingConnections()
        {
            try
            {
                while (true)
                {
                    _allDone.Reset();
                    if (_mainSocket != null)
                    {
                        _mainSocket.BeginAccept(new AsyncCallback(BeginAcceptCallback), _mainSocket);
                    }
                    _allDone.WaitOne();
                }
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }

        /// <summary>
        /// Accept Connection Callback. When an connection is found.
        /// </summary>
        /// <param name="result"></param>
        private void BeginAcceptCallback(IAsyncResult result)
        {
            try
            {
                _allDone.Set();

                // Organise the Async Result.
                Socket listener = (Socket)result.AsyncState;
                Socket handler = listener.EndAccept(result);

                // Add the connection to the manager.
                Manager.AddConnection(handler);

                // Raise the New Connection Found event.
                OnNewConnection(handler);

                State state = new State();
                state.socket = handler;

                // Begin receiveing data from the connection.
                while (true)
                {
                    _allDone.Reset();
                    handler.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), state);
                    _allDone.WaitOne();
                }
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }
        
        /// <summary>
        /// Receive data callback. When data has been sent to this socket.
        /// </summary>
        /// <param name="result"></param>
        private void BeginReceiveCallback(IAsyncResult result)
        {
            try
            { 
                _allDone.Set();

                // Organise the Async Result.
                State state = (State)result.AsyncState;
                Socket handler = state.socket;

                // Store the bytes recieved.
                int bytesRead = handler.EndReceive(result);

                // If there is data to be read.
                if (bytesRead > 0)
                {
                    // Store the revieved data.
                    state.stringBuilder.Clear();
                    state.stringBuilder.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Raise the Data Recieved Event.
                    OnConnectionRecieve(state);
                }
                // Not all data received. Get more.
                else
                {
                    handler.BeginReceive(state.buffer, 0, state.buffer.Length, 0, new AsyncCallback(BeginReceiveCallback), state);
                }
            }
            catch (SocketException ex)
            {
                OnError(ex);
            }
        }
        #endregion
    }
}
