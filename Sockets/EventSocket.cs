using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using kNet.Sockets.SocketEventArgs;

namespace kNet.Sockets
{
    public class EventSocket : Socket
    {
        #region Event Handlers
        /// <summary>
        /// Event when the socket is connecting to the host.
        /// </summary>
        public event EventHandler SocketConnecting;

        /// <summary>
        /// Event when the socket has connected to the host.
        /// </summary>
        public event EventHandler<SocketConnectEventArgs> SocketConnected;

        /// <summary>
        /// Event when the socket is in the process of disconnecting from the socket.
        /// </summary>
        public event EventHandler SocketDisconnecting;

        /// <summary>
        /// Event when the socket has disconnected from the socket.
        /// </summary>
        public event EventHandler<SocketDisconnectEventArgs> SocketDisconnected;

        /// <summary>
        /// Event when the socket has stated listening for new connections.
        /// </summary>
        public event EventHandler<SocketListenEventArgs> SocketListen;

        /// <summary>
        /// Event when the socket has started sending data to the host.
        /// </summary>
        public event EventHandler SocketSending;

        /// <summary>
        /// Event when the socket has finally sent data to the host.
        /// </summary>
        public event EventHandler<SocketSendEventArgs> SocketSend;

        /// <summary>
        /// Event when the socket is in the process of receiving data.
        /// </summary>
        public event EventHandler SocketReceiving;

        /// <summary>
        /// Event when the socket has received data.
        /// </summary>
        public event EventHandler<SocketReceiveEventArgs> SocketReceive;

        /// <summary>
        /// Event when the socket throws an error.
        /// </summary>
        public event EventHandler<SocketErrorEventArgs> SocketError;
        #endregion


        #region Constructors
        public EventSocket(SocketInformation socketInformation) : base(socketInformation) { }
        public EventSocket(SocketType socketType, ProtocolType protocolType) : base(socketType, protocolType) { }
        public EventSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType) : base(addressFamily, socketType, protocolType) { }
        #endregion


        #region Member methods
        public new void Connect(EndPoint endPoint)
        {
            try
            {
                OnSocketConnecting();
                base.Connect(endPoint);
                if (Connected)
                    OnSocketConnected(endPoint);
            } catch (SocketException ex)
            {
                OnSocketError(ex);
            }
        }

        public new void ConnectAsync(SocketAsyncEventArgs eventArgs)
        {
            try
            { 
                OnSocketConnecting();
                base.ConnectAsync(eventArgs);
                if (Connected)
                    OnSocketConnected(eventArgs.RemoteEndPoint);
            }
            catch (SocketException ex)
            {
                OnSocketError(ex);
            }
        }

        public new void Disconnect(bool reuseSocket)
        {
            try
            {
                OnSocketDisconnecting();
                base.Disconnect(reuseSocket);
                if (!Connected)
                    OnSocketDisconnected(reuseSocket);
            }
            catch (SocketException ex)
            {
                OnSocketError(ex);
            }
        }

        public new void DisconnectAsync(SocketAsyncEventArgs eventArgs)
        {
            try
            {
                OnSocketDisconnecting();
                base.DisconnectAsync(eventArgs);
                if (!Connected)
                    OnSocketDisconnected(eventArgs.DisconnectReuseSocket);
            }
            catch (SocketException ex)
            {
                OnSocketError(ex);
            }
        }

        public new void Send(byte[] buffer, SocketFlags socketFlags)
        {
            try
            {
                OnSocketSending();
                base.Send(buffer, 0, buffer.Length, SocketFlags.None);
                OnSocketSend();
            }
            catch (SocketException ex)
            {
                OnSocketError(ex);
            }
        }

        public int Recieve(byte[] readbyte)
        {
            try
            {
                OnSocketReceiving();
                int returnInt = base.Receive(readbyte);
                OnSocketReceive();
                return returnInt;
            }
            catch (SocketException ex)
            {
                OnSocketError(ex);
                throw new SocketException(ex.ErrorCode);
            }
        }

        public new void Listen(int backlog)
        {
            try
            {
                base.Listen(backlog);
                OnSocketListen(backlog, this.LocalEndPoint);
            }
            catch (SocketException ex)
            {
                OnSocketError(ex);
            }
        }

        public NetworkStream GetStream()
        {
            return new NetworkStream(this);
        }
        #endregion


        #region Event Definitions
        // Connecting
        protected virtual void OnSocketConnecting() => SocketConnecting?.Invoke(this, new EventArgs());
        protected virtual void OnSocketConnected(EndPoint endpoint)
        {
            SocketConnected?.Invoke(this, new SocketConnectEventArgs() { RemoteEndPoint = endpoint });
        }

        // Disconnecting
        protected virtual void OnSocketListen(int backlog, EndPoint endPoint)
        {
            SocketListen?.Invoke(this, new SocketListenEventArgs() { Backlog = backlog, LocalEndPoint = endPoint});
        }

        // Disconnecting
        protected virtual void OnSocketDisconnecting() => SocketDisconnecting?.Invoke(this, new EventArgs());
        protected virtual void OnSocketDisconnected(bool reuse)
        {
            SocketDisconnected?.Invoke(this, new SocketDisconnectEventArgs() { Reuse = reuse });
        }

        // Sending
        protected virtual void OnSocketSending() => SocketSending?.Invoke(this, new EventArgs());
        protected virtual void OnSocketSend() => SocketSend?.Invoke(this, new SocketSendEventArgs());

        // Recieving
        protected virtual void OnSocketReceiving() => SocketReceiving?.Invoke(this, new EventArgs());
        protected virtual void OnSocketReceive() => SocketReceive?.Invoke(this, new SocketReceiveEventArgs());

        // Error
        protected virtual void OnSocketError(SocketException ex)
        {
            SocketError?.Invoke(this, new SocketErrorEventArgs(ex));
        }
        #endregion
    }
}
