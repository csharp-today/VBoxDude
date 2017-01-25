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

        public string Name { get; }
        internal IProcessRunner ProcessRunner { get; }

        internal VirtualMachine(
            IConfiguration config,
            IProcessRunner runner,
            IDiskPathGetter pathGetter,
            string name)
        {
            _config = config;
            ProcessRunner = runner;
            _pathGetter = pathGetter;
            Name = name;
        }

        public async Task<IEnumerable<string>> GetDiskPathes()
        {
            return await _pathGetter.GetDiskPathAsync(Name);
        }

        public void Start()
        {
            ProcessRunner.RunAndContinue(_config.VirtualBoxHeadlessApp, "-s " + Name);
        }
    }
}
