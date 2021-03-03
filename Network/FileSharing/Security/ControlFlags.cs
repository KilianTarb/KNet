using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing.Security
{
    /// <summary>
    /// Control bits that qualify the meaning of a security descriptor (SD) or its individual members.
    /// </summary>
    public enum ControlFlags
    {
        /// <summary>
        /// Indicates an SD with a default owner security identifier (SID). Use this bit to find all of the objects that have default owner permissions set.
        /// </summary>
        SE_OWNER_DEFAULTED = 1,

        /// <summary>
        /// Indicates an SD with a default group SID. Use this bit to find all of the objects that have default group permissions set.
        /// </summary>
        SE_GROUP_DEFAULTED = 2,

        /// <summary>
        /// Indicates an SD that has a DACL. If this flag is not set, or if this flag is set and the DACL is NULL, the SD allows full access to everyone.
        /// </summary>
        SE_DACL_PRESENT = 4,

        /// <summary>
        /// Indicates an SD with a default DACL. For example, if an object creator does not specify a DACL, the object receives the default DACL from the access token of the creator. 
        /// This flag can affect how the system treats the DACL, with respect to access control entry (ACE) inheritance. The system ignores this flag if the SE_DACL_PRESENT flag is not set.
        /// </summary>
        SE_DACL_DEFAULTED = 8,

        /// <summary>
        /// Indicates an SD that has a system access control list (SACL).
        /// </summary>
        SE_SACL_PRESENT = 16,

        /// <summary>
        /// Indicates an SD with a default SACL. For example, if an object creator does not specify an SACL, the object receives the default SACL from the access token of the creator. 
        /// This flag can affect how the system treats the SACL, with respect to ACE inheritance. 
        /// The system ignores this flag if the SE_SACL_PRESENT flag is not set.
        /// </summary>
        SE_SACL_DEFAULTED = 32,

        /// <summary>
        /// Requests that the provider for the object protected by the SD automatically propagate the DACL to existing child objects. 
        /// If the provider supports automatic inheritance, the DACL is propagated to any existing child objects, and the SE_DACL_AUTO_INHERITED bit in the SD of the parent and child objects is set.
        /// </summary>
        SE_DACL_AUTO_INHERIT_REQ = 256,

        /// <summary>
        /// Requests that the provider for the object protected by the SD automatically propagate the SACL to existing child objects. 
        /// If the provider supports automatic inheritance, the SACL is propagated to any existing child objects, and the SE_SACL_AUTO_INHERITED bit in the SDs of the parent object and child objects is set.
        /// </summary>
        SE_SACL_AUTO_INHERIT_REQ = 512,

        /// <summary>
        /// Indicates an SD in which the DACL is set up to support automatic propagation of inheritable ACEs to existing child objects. 
        /// The system sets this bit when it performs the automatic inheritance algorithm for the object and its existing child objects.
        /// </summary>
        SE_DACL_AUTO_INHERITED = 1024,

        /// <summary>
        /// Indicates an SD in which the SACL is set up to support automatic propagation of inheritable ACEs to existing child objects. 
        /// The system sets this bit when it performs the automatic inheritance algorithm for the object and its existing child objects.
        /// </summary>
        SE_SACL_AUTO_INHERITED = 2048,

        /// <summary>
        /// Prevents the DACL of an SD from being modified by inheritable ACEs.
        /// </summary>
        SE_DACL_PROTECTED = 4096,

        /// <summary>
        /// Prevents the SACL of an SD from being modified by inheritable ACEs.
        /// </summary>
        SE_SACL_PROTECTED = 8192,

        /// <summary>
        /// Indicates an SD in self-relative format with all the security information in a contiguous block of memory. If this flag is not set, the SD is in absolute format. For more information.
        /// </summary>
        SE_SELF_RELATIVE = 32768,
    }
}
