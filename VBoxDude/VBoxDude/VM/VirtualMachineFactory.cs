using Microsoft.Practices.Unity;
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
        private IUnityContainer _container;
        private IVirtualMachineImporter _importer;
        private IProcessRunner _runner;

        private IVirtualMachineImporter Importer
        {
            get
            {
                if (_importer == null)
                {
                    _importer = _container.Resolve<IVirtualMachineImporter>();
                }
                return _importer;
            }
        }

        public VirtualMachineFactory(
            IConfiguration config,
            IUnityContainer container,
            IProcessRunner runner)
        {
            _config = config;
            _container = container;
            _runner = runner;
        }

        public VirtualMachine CreateFromName(string name)
        {
            var vm = new VirtualMachine(_config, _runner, name);
            return vm;
        }

        public VirtualMachine ImportFromFile(string filePath, string name)
        {
            var vm = Importer.Import(filePath, name);
            return vm;
        }
    }
}
