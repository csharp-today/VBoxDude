using System;
using System.Linq;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.FileSystem;
using VBoxDude.PorcessRunner;

namespace VBoxDude.VM.Disks
{
    internal class DiskUuidGetter : IDiskUuidGetter
    {
        public const string UuidIdentifier = "UUID:";

        private IConfiguration _config;
        private IFileSystem _fileSystem;
        private IProcessRunner _runner;

        public DiskUuidGetter(
            IConfiguration config,
            IFileSystem fileSystem,
            IProcessRunner runner)
        {
            _config = config;
            _fileSystem = fileSystem;
            _runner = runner;
        }

        public async Task<string> GetDiskUuidAsync(string filePath)
        {
            if (!_fileSystem.FileExists(filePath))
            {
                throw new ArgumentException("File does not exist: " + filePath);
            }

            var result = await _runner.RunAsync(_config.VirtualBoxManagerApp, $"showhdinfo \"{filePath}\"");

            if (result.StandardOutput == null)
            {
                return null;
            }

            var uuidLine = result.StandardOutput.First(line => line.StartsWith(UuidIdentifier));
            if (uuidLine == null)
            {
                return null;
            }

            var uuid = uuidLine.Replace(UuidIdentifier, string.Empty).Trim();
            return uuid;
        }
    }
}