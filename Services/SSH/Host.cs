using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace kNet.Services.SSH
{
    public class Host
    {
        public IPEndPoint RemoteEndPoint { get; set; }
        public string Username { get; set; }


        public Host(IPEndPoint remoteEndPoint)
        {
            this.RemoteEndPoint = remoteEndPoint;
            this.Username = string.Empty;
        }

        public Host(IPEndPoint remoteEndPoint, string username)
        {
            this.RemoteEndPoint = remoteEndPoint;
            this.Username = username;
        }
    }
}
