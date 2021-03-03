using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Timers;

namespace kNet.Sockets
{
    public class ConnectionManager
    {
        public Socket Server { get; private set; }
        public List<Connection> Connections = new List<Connection>();

        private Timer _timer;

        #region Constructors
        public ConnectionManager(Socket server)
        {
            Server = server;

            _timer = new Timer();
            _timer.Enabled = true;
            _timer.Start();
            _timer.Elapsed += _timer_Elapsed;
        }
        #endregion

        #region Events
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CheckConnections();
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Finds connection based on a local or remote endpoint.
        /// </summary>
        /// <param name="endPoint">Local or remote endpoint.</param>
        /// <returns></returns>
        public Connection FindConnection(IPEndPoint endPoint)
        {
            for (int i=0; i<Connections.Count; i++)
            {
                //IPEndPoint local = Connections[i].LocalEndPoint;
                IPEndPoint remote = Connections[i].RemoteEndPoint;

                if (remote == endPoint)
                {
                    return Connections[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a new connection.
        /// </summary>
        /// <param name="client">The socket that you want to add.</param>
        public void AddConnection(Socket client)
        {
            //if (FindConnection(client.RemoteEndPoint as IPEndPoint) != null)
            //{
                Connections.Add(new Connection(client));
            //}
        }

        private void CheckConnections()
        {
            for (int i=0; i<Connections.Count; i++)
            {
                Connections[i].CheckStatus();
            }
        }
        #endregion
    }

    public class Connection
    {
        #region Member Variables
        public Socket Client { get; private set; }
        public IPEndPoint LocalEndPoint { get; private set; }
        public IPEndPoint RemoteEndPoint { get; private set; }
        public ConnectionStatus Status { get; private set; }

        #endregion

        #region Constructors
        public Connection(Socket client)
        {
            Client = client;
            LocalEndPoint = Client.LocalEndPoint as IPEndPoint;
            RemoteEndPoint = Client.RemoteEndPoint as IPEndPoint;
            Status = ConnectionStatus.Unkown;   
        }
        #endregion

        #region Events

        #endregion

        #region Member Methods
        public void CheckStatus()
        {
            if (Client.Connected)
            {
                Status = ConnectionStatus.Connected;
            }
            else
            {
                Status = ConnectionStatus.Disconnected;
            }
        }
        #endregion
    }

    public enum ConnectionStatus
    {
        Connected = 0,
        Disconnected = 1,
        Unkown = 2,
    }
}
