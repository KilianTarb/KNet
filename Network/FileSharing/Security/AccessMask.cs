using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing.Security
{
    /// <summary>
    /// A 32-bit value that specifies the rights that are allowed or denied in an access control entry (ACE). 
    /// An access mask is also used to request access rights when an object is opened.
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/secrcw32prov/win32-ace#members
    /// </summary>
    public enum AccessMask
    {
        /// <summary>
        /// Grants the right to read data from the file. For a directory, this value grants the right to list the contents of the directory.
        /// </summary>
        FILE_READ_DATA = 1,
        
        /// <summary>
        /// Grants the right to write data to the file. For a directory, this value grants the right to create a file in the directory.
        /// </summary>
        FILE_WRITE_DATA = 2,

        /// <summary>
        /// Grants the right to append data to the file. For a directory, this value grants the right to create a subdirectory.
        /// </summary>
        FILE_APPEND_DATA = 4,

        /// <summary>
        /// Grants the right to read extended attributes.
        /// </summary>
        FILE_READ_EA = 8,

        /// <summary>
        /// Grants the right to write extended attributes.
        /// </summary>
        FILE_WRITE_EA = 16,

        /// <summary>
        /// Grants the right to execute a file. For a directory, the directory can be traversed.
        /// </summary>
        FILE_EXECUTE = 32,

        /// <summary>
        /// Grants the right to delete a directory and all the files it contains (its children), even if the files are read-only.
        /// </summary>
        FILE_DELETE_CHILD = 64,

        /// <summary>
        /// Grants the right to read file attributes.
        /// </summary>
        FILE_READ_ATTRIBUTES = 128,

        /// <summary>
        /// Grants the right to change file attributes.
        /// </summary>
        FILE_WRITE_ATTRIBUTES = 256,


        /// <summary>
        /// Standard Access Right. Grants delete access.
        /// </summary>
        DELETE = 65536,

        /// <summary>
        /// Standard Access Right. Grants read access to the security descriptor and owner.
        /// </summary>
        READ_CONTROL = 131072,

        /// <summary>
        /// Standard Access Right. Grants write access to the discretionary access control list (ACL).
        /// </summary>
        WRITE_DAC = 262144,

        /// <summary>
        /// Standard Access Right. Assigns the write owner.
        /// </summary>
        WRITE_OWNER = 524288,

        /// <summary>
        /// Standard Access Right. Synchronizes access and allows a process to wait for an object to enter the signaled state.
        /// </summary>
        SYNCHRONIZE = 1048576,


        /// <summary>
        /// Generic Access Right.
        /// </summary>
        FULL_CONTROL = 2032127,

        /// <summary>
        /// Generic Access Right.
        /// </summary>
        READ = 1179817,

        /// <summary>
        /// Generic Access Right.
        /// </summary>
        MODIFY = 1245631,
    }

    /// <summary>
    /// https://docs.microsoft.com/en-gb/windows/desktop/SecAuthZ/standard-access-rights
    /// </summary>
    public enum StandardAccessRight
    {
        /// <summary>
        /// Standard Access Right. Grants delete access.
        /// </summary>
        DELETE = 65536,

        /// <summary>
        /// Standard Access Right. Grants read access to the security descriptor and owner.
        /// </summary>
        READ_CONTROL = 131072,

        /// <summary>
        /// Standard Access Right. Grants write access to the discretionary access control list (ACL).
        /// </summary>
        WRITE_DAC = 262144,

        /// <summary>
        /// Standard Access Right. Assigns the write owner.
        /// </summary>
        WRITE_OWNER = 524288,

        /// <summary>
        /// Standard Access Right. Synchronizes access and allows a process to wait for an object to enter the signaled state.
        /// </summary>
        SYNCHRONIZE = 1048576,
    }

    /// <summary>
    /// https://docs.microsoft.com/en-gb/windows/desktop/SecAuthZ/generic-access-rights
    /// </summary>
    public enum GenericAccessRight
    {
        /// <summary>
        /// Generic Access Right.
        /// </summary>
        FULL_CONTROL = 2032127,

        /// <summary>
        /// Generic Access Right.
        /// </summary>
        READ = 1179817,

        /// <summary>
        /// Generic Access Right.
        /// </summary>
        MODIFY = 1245631,
    }
}
