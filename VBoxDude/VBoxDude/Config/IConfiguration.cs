using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBoxDude.Config
{
    public interface IConfiguration
    {
        string VirtualBoxHeadlessApp { get; }
    }
}
