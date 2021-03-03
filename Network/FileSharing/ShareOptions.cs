using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNet.Network.FileSharing
{
    /// <summary>
    /// Contains all the options to create a share. Null values will not matter in creation.
    /// </summary>
    public class ShareOptions
    {
        /// <summary>
        /// Path to the source folder.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Display name of the share.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of share.
        /// </summary>
        public ShareType Type { get; set; }

        /// <summary>
        /// The maximum amount of people allowed in the share at one time.
        /// </summary>
        public uint MaximumAllowed { get; set; }

        /// <summary>
        /// The description of the share.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// The access password of the share.
        /// </summary>
        public string Password { get; set; }


        public ShareOptions(string path, string name, ShareType type)
        {
            Path = path;
            Name = name;
            Type = type;
        }
    }
}
