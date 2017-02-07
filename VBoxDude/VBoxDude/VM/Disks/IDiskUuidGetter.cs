using System.Threading.Tasks;

namespace VBoxDude.VM.Disks
{
    internal interface IDiskUuidGetter
    {
        Task<string> GetDiskUuidAsync(string filePath);
    }
}