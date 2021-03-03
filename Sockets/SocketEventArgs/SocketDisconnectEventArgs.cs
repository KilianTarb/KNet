using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Sockets.SocketEventArgs
{
    public class SocketDisconnectEventArgs : EventArgs
    {
        public bool Reuse { get; set; }
    }
}
