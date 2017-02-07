using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.PorcessRunner;
using VBoxDude.VM.Disks;

namespace VBoxDude.VM
{
    public class VirtualMachine
    {
        private IConfiguration _config;
        private IDiskPathGetter _pathGetter;
        private IDiskUuidGetter _uuidGetter;

        public string Name { get; }
        internal IProcessRunner ProcessRunner { get; }

        internal VirtualMachine(
            IConfiguration config,
            IProcessRunner runner,
            IDiskPathGetter pathGetter,
            IDiskUuidGetter uuidGetter,
            string name)
        {
            _config = config;
            ProcessRunner = runner;
            _pathGetter = pathGetter;
            _uuidGetter = uuidGetter;
            Name = name;
        }

        public async Task<IEnumerable<string>> GetDiskPathsAsync()
        {
            return await _pathGetter.GetDiskPathsAsync(Name);
        }

        internal async Task<string> GetDiskUuidAsync(string filePath)
        {
            return await _uuidGetter.GetDiskUuidAsync(filePath);
        }

        public void Start()
        {
            ProcessRunner.RunAndContinue(_config.VirtualBoxHeadlessApp, "-s " + Name);
        }
    }
}
