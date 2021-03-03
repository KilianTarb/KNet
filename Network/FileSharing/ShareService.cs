using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security;
using System.Security.AccessControl;
using System.IO;
using kNet.Network.FileSharing.Security;

namespace kNet.Network.FileSharing
{
    /// <summary>
    /// For information:
    /// https://docs.microsoft.com/en-us/windows/desktop/cimwin32prov/win32-share
    /// </summary>
    public class ShareService
    {
        private static ManagementClass _shareClass = new ManagementClass("Win32_Share");

        public ShareService() { }


        #region Find Share
        public Win32Share[] GetShares()
        {
            ManagementObjectCollection shares = _shareClass.GetInstances();

            List<Win32Share> win32Shares = new List<Win32Share>();
            foreach (ManagementObject single in shares)
            {
                Win32Share s = new Win32Share(single);

                //s.InstallDate = (DateTime)single["InstallDate"];
                //s.AccessMask = (uint)single["AccessMask"];
                win32Shares.Add(s);
            }
            return win32Shares.ToArray();
        }
        public static ManagementObject FindShare(string name)
        {
            ManagementObjectCollection shares = _shareClass.GetInstances();
            foreach (ManagementObject single in shares)
            {
                if (name == (string)single["Name"])
                {
                    return single;
                }
            }
            return null;
        }
        #endregion

        #region Create Share
        /// <summary>
        /// Creates a new networked share.
        /// </summary>
        /// <param name="path">Path to the source.</param>
        /// <param name="name">Name of the share.</param>
        /// <param name="type">The type of share.</param>
        public void CreateShare(string path, string name, ShareType type)
        {
            ManagementBaseObject inParams = _shareClass.GetMethodParameters("Create");
            inParams["Path"] = path;
            inParams["Name"] = name;
            inParams["Type"] = type;

            ManagementBaseObject outParams = _shareClass.InvokeMethod("Create", inParams, null);
            if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                throw new Exception("Unable to share directory.");
        }

        /// <summary>
        /// Creates a new networked share.
        /// </summary>
        /// <param name="path">Path to the source.</param>
        /// <param name="name">Name of the share.</param>
        /// <param name="description">Share's description.</param>
        /// <param name="type">The type of share.</param>
        public void CreateShare(string path, string name, string description, ShareType type)
        {
            ManagementBaseObject inParams = _shareClass.GetMethodParameters("Create");
            inParams["Path"] = path;
            inParams["Name"] = name;
            inParams["Type"] = type;
            inParams["Description"] = description;

            ManagementBaseObject outParams = _shareClass.InvokeMethod("Create", inParams, null);
            if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                throw new Exception("Unable to share directory.");
        }

        /// <summary>
        /// Creates a new secure networked share.
        /// </summary>
        /// <param name="path">Path to the source.</param>
        /// <param name="name">Name of the share.</param>
        /// <param name="description">Share's description.</param>
        /// <param name="type">The type of share.</param>
        /// <param name="password">The share's access password</param>
        /// <param name="maximumAllowed">The maximum amount of users allowed at one time.</param>
        public void CreateShare(string path, string name, string description, ShareType type, string password, uint maximumAllowed)
        {
            ManagementBaseObject inParams = _shareClass.GetMethodParameters("Create");
            inParams["Path"] = path;
            inParams["Name"] = name;
            inParams["Type"] = type;
            inParams["Description"] = description;
            inParams["Password"] = password;
            inParams["MaximumAllowed"] = maximumAllowed;

            ManagementBaseObject outParams = _shareClass.InvokeMethod("Create", inParams, null);
            if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                throw new Exception("Unable to share directory.");
        }

        /// <summary>
        /// Creates a new secure networked share.
        /// </summary>
        /// <param name="options">Configuration options for the share.</param>
        public void CreateShare(ShareOptions options)
        {
            ManagementBaseObject inParams = _shareClass.GetMethodParameters("Create");
            inParams["Path"] = options.Path;
            inParams["Name"] = options.Name;
            inParams["Type"] = options.Type;

            if (options.Description != null)
                inParams["Description"] = options.Description;

            if (options.Password != null)
                inParams["Password"] = options.Password;

            if (options.MaximumAllowed != 0)
                inParams["MaximumAllowed"] = options.MaximumAllowed;

            ManagementBaseObject outParams = _shareClass.InvokeMethod("Create", inParams, null);
            if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                throw new Exception("Unable to share directory.");
        }
        #endregion

        #region Delete Share
        public ShareErrorCode DeleteShare(string name)
        {
            ManagementObjectCollection shares = _shareClass.GetInstances();

            foreach (ManagementObject single in shares)
            {
                if (name == (string)single["Name"])
                {
                    object result = single.InvokeMethod("Delete", new object[] { });
                    int resultCode = Convert.ToInt32(result);
                    return (ShareErrorCode)resultCode;
                }
            }

            return ShareErrorCode.UnkownNetName;
        }
        #endregion

        #region Get Access Mask
        public static AccessMask GetAccessMask(string ShareName)
        {
            ManagementObject share = FindShare(ShareName);

            if (share == null)
                throw new NullReferenceException();

            object result = share.InvokeMethod("GetAccessMask", new object[] { });
            int resultCode = Convert.ToInt32(result);
            return (AccessMask)resultCode;
        }
        #endregion

        #region Set Share Info
        /// <summary>
        /// Sets the Access of the share.
        /// </summary>
        /// <param name="ShareName"></param>
        /// <param name="Access"></param>
        public void SetShareInfo(string ShareName, SecurityDescriptor Access)
        {
            ManagementBaseObject inParams = _shareClass.GetMethodParameters("SetShareInfo");
        }

        /// <summary>
        /// Sets the maximum amount of users that can connect to the share.
        /// </summary>
        /// <param name="ShareName"></param>
        /// <param name="MaximumAllowed"></param>
        public void SetShareInfo(string ShareName, uint MaximumAllowed)
        {
            ManagementObject share = FindShare(ShareName);

            if (share == null)
                throw new NullReferenceException();

            ManagementBaseObject inParams = _shareClass.GetMethodParameters("SetShareInfo");
            inParams["MaximumAllowed"] = MaximumAllowed;
        }

        /// <summary>
        /// Sets the share's description.
        /// </summary>
        /// <param name="ShareName"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public static ShareErrorCode SetShareInfo(string ShareName, string Description)
        {
            ManagementObject share = FindShare(ShareName);

            if (share == null)
                throw new NullReferenceException();

            ManagementBaseObject inParams = share.GetMethodParameters("SetShareInfo");
            inParams["Description"] = Description;

            object result = share.InvokeMethod("SetShareInfo", new object[] { null, Description, null });
            int resultCode = Convert.ToInt32(result);
            return (ShareErrorCode)resultCode;
        }

        /// <summary>
        /// Sets the share's info.
        /// </summary>
        /// <param name="ShareName"></param>
        /// <param name="MaximumAllowed"></param>
        /// <param name="Description"></param>
        /// <param name="Access"></param>
        public void SetShareInfo(string ShareName, uint MaximumAllowed, string Description, SecurityDescriptor Access)
        {
            ManagementBaseObject inParams = _shareClass.GetMethodParameters("SetShareInfo");
        }
        #endregion

        #region Set Access
        public static void AddAccessRule(Win32Share directory, string user, FileSystemRights right, AccessControlType controlType)
        {
            FileSecurity fsec = File.GetAccessControl(directory.Path);
            fsec.AddAccessRule(new FileSystemAccessRule(user, right, controlType));
            File.SetAccessControl(directory.Path, fsec);
        }
        public static void RemoveAccessRule(Win32Share directory, string user, FileSystemRights right, AccessControlType controlType)
        {
            FileSecurity fsec = File.GetAccessControl(directory.Path);
            fsec.RemoveAccessRule(new FileSystemAccessRule(user, right, controlType));
            File.SetAccessControl(directory.Path, fsec);
        }
        #endregion
    }

    public enum ShareErrorCode
    {
        Success = 0,
        AccessDenied = 2,
        UnkownFailure = 8,
        InvaildName = 9,
        InvalidLevel = 10,
        InvalidParameter = 21,
        DeuplicateShare = 22,
        RedirectedPath = 23,
        UnkownDirectory = 24,
        UnkownNetName = 25,
        Other = 26,
    }
}
