using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace kNet.Sockets
{
    public static class SocketErrorProvider
    {
        /// <summary>
        /// Gets string which describes the SocketError.
        /// </summary>
        /// <param name="ex">The socket exception that contains the error code.</param>
        /// <returns></returns>
        public static string GetErrorString(SocketException ex)
        {
            switch (ex.SocketErrorCode)
            {
                case SocketError.Interrupted:
                    return "Operation interrupted.";
                case SocketError.AccessDenied:
                    return "Access Denied.";
                case SocketError.Fault:
                    return "Invailed Address.";
                case SocketError.InvalidArgument:
                    return "Invailed Argument was supplied.";
                case SocketError.TooManyOpenSockets:
                    return "Too many open sockets in the underlying socket provider.";
                case SocketError.WouldBlock:
                    return "An opertation on a blocking socket could not be completed immediately";
                case SocketError.InProgress:
                    return "A blocking operation in progress";
                case SocketError.AlreadyInProgress:
                    return "There is already an operation in progress.";
                case SocketError.NotSocket:
                    return "An opertaion was attempted on a non-socket.";
                case SocketError.DestinationAddressRequired:
                    return "Destination address required.";
                case SocketError.MessageSize:
                    return "Message size too long.";
                case SocketError.ProtocolType:
                    return "Invaild protocol type.";
                case SocketError.ProtocolOption:
                    return "Invaild protocol option.";
                case SocketError.ProtocolNotSupported:
                    return "Protocol not supported.";
                case SocketError.SocketNotSupported:
                    return "Socket type not supported.";
                case SocketError.OperationNotSupported:
                    return "Address family not supported by the protocol family.";
                case SocketError.ProtocolFamilyNotSupported:
                    return "Protocol family no configured.";
                case SocketError.AddressFamilyNotSupported:
                    return "Address family not supported.";
                case SocketError.AddressAlreadyInUse:
                    return "Address family already in use, only on address family is permmitted for use.";
                case SocketError.AddressNotAvailable:
                    return "Address not avaliable.";
                case SocketError.NetworkDown:
                    return "Network is down.";
                case SocketError.NetworkUnreachable:
                    return "Network is unreachable.";
                case SocketError.NetworkReset:
                    return "Network reset, operation timed out.";
                case SocketError.ConnectionAborted:
                    return "Connection was aborted.";
                case SocketError.ConnectionReset:
                    return "Connection was reset.";
                case SocketError.NoBufferSpaceAvailable:
                    return "No buffer space avaliable.";
                case SocketError.IsConnected:
                    return "The socket is already connected.";
                case SocketError.NotConnected:
                    return "The socket is not connected.";
                case SocketError.Shutdown:
                    return "The socket has been shutdown.";
                case SocketError.TimedOut:
                    return "Connection attempt timed out.";
                case SocketError.ConnectionRefused:
                    return "Host actively refused connection.";
                case SocketError.HostDown:
                    return "The host is down.";
                case SocketError.HostUnreachable:
                    return "The host is unreachable.";
                case SocketError.ProcessLimit:
                    return "Too many processes in the underlying socket provider.";
                case SocketError.SystemNotReady:
                    return "Netwokr subsystem is unavaliable.";
                case SocketError.VersionNotSupported:
                    return "Socket version is not supported.";
                case SocketError.NotInitialized:
                    return "Socket not initialized.";
                case SocketError.Disconnecting:
                    return "Disconnection is in progress.";
                case SocketError.TypeNotFound:
                    return "Class not found.";
                case SocketError.HostNotFound:
                    return "No such host is known.";
                case SocketError.TryAgain:
                    return "Hostname could not be resolved.";
                case SocketError.NoRecovery:
                    return "Could not display error.";
                case SocketError.NoData:
                    return "Could not find host on name server.";
                case SocketError.IOPending:
                    return "Overlapped operation detected.";
                case SocketError.OperationAborted:
                    return "Operation has been aborted due the closure of socket.";
                default:
                    return "A socket error occured.";
            }
        }
    }
}
