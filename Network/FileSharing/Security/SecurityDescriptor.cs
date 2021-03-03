using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing.Security
{
    /// <summary>
    /// A security descriptor contains the security information for a securable object.
    /// <see cref="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/secrcw32prov/win32-securitydescriptor"/>
    /// </summary>
    public class SecurityDescriptor
    {
        private UInt64 TimeCreated { get; set; }
        public ControlFlags ControlFlags { get; set; }
        public AccessControlEntry[] DACL { get; set; }
        public AccessControlEntry[] SACL { get; set; }
        public Trustee Group { get; set; }
        public Trustee Owner { get; set; }
    }
}
