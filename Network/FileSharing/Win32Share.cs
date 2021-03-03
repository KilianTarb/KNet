using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using kNet.Network.FileSharing.Security;

namespace kNet.Network.FileSharing
{
    public class Win32Share
    {
        #region Properties
        public string Name { get; private set; }
        public string Caption { get; private set; }
        public string Description { get; private set; }
        public string Path { get; private set; }
        public bool MaximumAllowed { get; private set; }
        public ShareType Type { get; private set; }
        public DateTime InstallDate { get; private set; }
        public AccessMask AccessMask { get; private set; }
        public SecurityDescriptor SecurityDescriptor { get; private set; }

        private ManagementObject ShareInstance { get; set; }
        #endregion

        #region Constructor
        public Win32Share(ManagementObject shareInstance)
        {
            ShareInstance = shareInstance;

            Name = (string)shareInstance["Name"];
            Caption = (string)shareInstance["Caption"];
            Description = (string)shareInstance["Description"];
            Path = (string)shareInstance["Path"];
            MaximumAllowed = (bool)shareInstance["AllowMaximum"];
            Type = (ShareType)shareInstance["Type"];

            AccessMask = GetAccessMask();
        }
        #endregion

        #region WMI Methods
        /// <summary>
        /// Deletes the file share.
        /// </summary>
        /// <returns></returns>
        internal ShareErrorCode DeleteShare()
        {
            return ShareErrorCode.Success;
        }

        /// <summary>
        /// Gets the Access Mask.
        /// </summary>
        /// <returns></returns>
        internal AccessMask GetAccessMask()
        {
            ManagementBaseObject result = ShareInstance.InvokeMethod("GetAccessMask", null, null);
            int resultCode = Convert.ToInt32(result);
            return (AccessMask)resultCode;
        }
        #endregion

        #region Set Methods
        /// <summary>
        /// Sets the Net Name of the share.
        /// </summary>
        /// <returns></returns>
        public ShareErrorCode SetName()
        {
            return ShareErrorCode.Success;
        }

        /// <summary>
        /// Sets the description of the share.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public ShareErrorCode SetDescription(string description)
        {
            ManagementBaseObject inParams = ShareInstance.GetMethodParameters("SetShareInfo");
            inParams["Description"] = Description;

            object result = ShareInstance.InvokeMethod("SetShareInfo", new object[] { null, Description, null });
            int resultCode = Convert.ToInt32(result);
            return (ShareErrorCode)resultCode;
        }

        /// <summary>
        /// Sets the maximum amount of users allowed to be connected to the share.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public ShareErrorCode SetMaximumAllowed(int max)
        {
            object result = ShareInstance.InvokeMethod("SetShareInfo", new object[] { max, null, null });
            int resultCode = Convert.ToInt32(result);
            return (ShareErrorCode)resultCode;
        }

        /// <summary>
        /// Sets the security object of the share.
        /// </summary>
        /// <param name="newDescriptor"></param>
        /// <returns></returns>
        public ShareErrorCode SetSecurityDescriptor(SecurityDescriptor newDescriptor)
        {
            return ShareErrorCode.Success;
        }
        #endregion
    }
}
