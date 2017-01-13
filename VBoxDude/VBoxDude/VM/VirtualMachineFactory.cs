using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.PorcessRunner;

namespace VBoxDude.VM
{
    public class VirtualMachineFactory
    {
        private IConfiguration _config;
        private IProcessRunner _runner;

        public VirtualMachineFactory(IConfiguration config, IProcessRunner runner)
        {
            _config = config;
            _runner = runner;
        }

        public VirtualMachine CreateFromName(string name)
        {
            var vm = new VirtualMachine(_config, _runner, name);
            return vm;
        }
    }
}
