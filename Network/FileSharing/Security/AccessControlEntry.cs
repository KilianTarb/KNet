using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing.Security
{
    /// <summary>
    /// An ACE grants permission to execute a restricted operation, such as writing to a file or formatting a disk.
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/secrcw32prov/win32-ace
    /// </summary>
    public class AccessControlEntry
    {
        public UInt64 TimeCreated { get; set; }
        public AccessMask AccessMask { get; set; }
        public AccessControlEntryFlags AceFlags { get; set; }
        public AccessControlEntryType AceType { get; set; }
        public string GuidInheritedObjectType { get; set; }
        public string GuidObjectType { get; set; }
        public Trustee Trustee { get; set; }
    }
}
