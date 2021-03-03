using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace kNet.Sockets.SocketEventArgs
{
    public class SocketListenEventArgs : EventArgs
    {
        public int Backlog { get; set; }
        public EndPoint LocalEndPoint { get; set; }
    }
}
