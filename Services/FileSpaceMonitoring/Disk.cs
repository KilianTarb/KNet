using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kNet.Services.FileSpaceMonitoring
{
    class Disk
    {
        public int MaxSpace { get; set; }
        public int MinSpace { get; set; }
        public int SpaceAvailable { get; set; }
        public int SpaceAvailablePercent { get; set; }
        public string DriveType { get; set; }
        public string DriveLocation { get; set; }

        private DriveInfo Drive { get; set; }

        public Disk(DriveInfo driveInfo)
        {
            Drive = driveInfo;
        }
    }
}
