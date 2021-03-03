using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace kNet.Sockets.SocketEventArgs
{
    public class SocketNewConnectionEventArgs
    {
        public Socket SocketHandler { get; private set; }
        public IPEndPoint ConnectionTo { get; private set; }
        public IPEndPoint ConnectionFrom { get; private set; }

        public SocketNewConnectionEventArgs(Socket socketHandler)
        {
            SocketHandler = socketHandler;
            ConnectionTo = SocketHandler.LocalEndPoint as IPEndPoint;
            ConnectionFrom = SocketHandler.RemoteEndPoint as IPEndPoint;
        }
    }
}
