using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace kNet.Sockets.SocketEventArgs
{
    public class SocketConnectEventArgs : EventArgs
    {
        public EndPoint RemoteEndPoint { get; set; }
    }
}
