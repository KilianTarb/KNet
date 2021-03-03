using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using kNet.Network;
using kNet.Network.Interop;

namespace kNet
{
    public class Machine
    {
        public Machine() { }

        /// <summary>
        /// The currently connected network.
        /// </summary>
        public Network.Network Network
        {
            get
            {
                kNet.Network.Network connectedNetwork = null;
                foreach (kNet.Network.Network n in NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected))
                {
                    connectedNetwork = n;
                }
                return connectedNetwork;
            }
        }

        /// <summary>
        /// Gets all the machine's Network Interfaces.
        /// </summary>
        public System.Net.NetworkInformation.NetworkInterface[] NetworkInterfaces
        {
            get
            {
                return System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            }
        }

        /// <summary>
        /// Returns this computer's hostname.
        /// </summary>
        public string Name
        {
            get
            {
                IPGlobalProperties computer = IPGlobalProperties.GetIPGlobalProperties();
                return computer.HostName;
            }
        }

        /// <summary>
        /// Returns a bool whether the machine is connected the internet.
        /// </summary>
        public bool IsConnectedToInternet
        {
            get
            {
                return this.Network.IsConnectedToInternet;
            }
        }
    }
}
