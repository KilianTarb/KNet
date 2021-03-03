using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Messaging;

namespace kNet.Network.FileSharing.Security
{
    /// <summary>
    /// Specifies a trustee that can be a name or a security identifier (SID) byte array.
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/secrcw32prov/win32-trustee
    /// </summary>
    public class Trustee : System.Messaging.Trustee
    {
        public UInt64 TimeCreated { get; set; }
        public string Domain { get; set; }
        public byte[] SID { get; set; }
        public uint SidLength { get; set; }
        public string SIDString { get; set; }

        private ManagementClass _trusteeClass = new ManagementClass("Win32_Trustee");
        
        public Trustee()
        {
        }
    }
}
