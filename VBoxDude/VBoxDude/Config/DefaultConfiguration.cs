using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBoxDude.Config
{
    public class DefaultConfiguration : IConfiguration
    {
        private const string _virtualBoxPath = @"C:\Program Files\Oracle\VirtualBox";

        public string VirtualBoxHeadlessApp { get; } = Path.Combine(_virtualBoxPath, "VBoxHeadless.exe");
        public string VirtualBoxManagerApp { get; } = Path.Combine(_virtualBoxPath, "VBoxManage.exe");
    }
}
