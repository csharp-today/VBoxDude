using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.PorcessRunner;

namespace VBoxDude.VM
{
    public class VirtualMachine
    {
        private IConfiguration _config;

        public string Name { get; }
        public IProcessRunner ProcessRunner { get; }

        internal VirtualMachine(
            IConfiguration config,
            IProcessRunner runner,
            string name)
        {
            _config = config;
            ProcessRunner = runner;
            Name = name;
        }

        public void Start()
        {
            ProcessRunner.RunAndContinue(_config.VirtualBoxHeadlessApp, "-s " + Name);
        }
    }
}
