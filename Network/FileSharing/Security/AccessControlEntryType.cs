using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing.Security
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/secrcw32prov/win32-ace#members
    /// </summary>
    public enum AccessControlEntryType
    {
        AccessAllowed = 0,
        AccessDenied = 1,
        Audit = 2,
    }
}
