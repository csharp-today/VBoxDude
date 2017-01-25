using System.Collections.Generic;
using System.Threading.Tasks;

namespace VBoxDude.VM.Disks
{
    internal interface IDiskPathGetter
    {
        Task<IEnumerable<string>> GetDiskPathsAsync(string machineName);
    }
}