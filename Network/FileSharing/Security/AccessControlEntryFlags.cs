using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing.Security
{
    public enum AccessControlEntryFlags
    {
        /// <summary>
        /// Noncontainer child objects inherit the ACE as an effective ACE.
        /// </summary>
        OBJECT_INHERIT_ACE = 1,

        /// <summary>
        /// Child objects that are containers, such as directories, inherit the ACE as an effective ACE. 
        /// The inherited ACE is inheritable unless the NO_PROPAGATE_INHERIT_ACE bit flag is also set.
        /// </summary>
        CONTAINER_INHERIT_ACE = 2,

        /// <summary>
        /// If the ACE is inherited by a child object, the system clears the OBJECT_INHERIT_ACE and CONTAINER_INHERIT_ACE flags in the inherited ACE. 
        /// This prevents the ACE from being inherited by subsequent generations of objects.
        /// </summary>
        NO_PROPAGATE_INHERIT_ACE = 4,

        /// <summary>
        /// Indicates an inherit-only ACE which does not control access to the object to which it is attached. 
        /// If this flag is not set, the ACE is an effective ACE which controls access to the object to which it is attached.
        /// </summary>
        INHERIT_ONLY_ACE = 8,

        /// <summary>
        /// The two possible values for AceFlags that pertain only to an ACE contained within a system access control list (SACL) are listed below.
        /// </summary>
        INHERITED_ACE = 16,

        /// <summary>
        /// Used with system-audit ACEs in an SACL to generate audit messages for successful access attempts.
        /// </summary>
        SUCCESSFUL_ACCESS_ACE_FLAG = 64,

        /// <summary>
        /// Used with system-audit ACEs in an SACL to generate audit messages for failed access attempts.
        /// </summary>
        FAILED_ACCESS_ACE_FLAG = 128,
    }
}
