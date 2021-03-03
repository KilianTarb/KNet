using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace kNet.Sockets.SocketEventArgs
{
    public class SocketErrorEventArgs : EventArgs
    {
        public SocketException SocketException { get; private set; }

        public SocketErrorEventArgs(SocketException socketException)
        {
            SocketException = socketException;
        }
    }
}
