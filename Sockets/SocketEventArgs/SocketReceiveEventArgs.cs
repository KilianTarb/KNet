using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kNet.Sockets;

namespace kNet.Sockets.SocketEventArgs
{
    public class SocketReceiveEventArgs : EventArgs
    {
        public State ReceiveState { get; set; }
    }
}
