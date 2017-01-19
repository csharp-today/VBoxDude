using System;
using VBoxDude.Config;
using VBoxDude.FileSystem;
using VBoxDude.PorcessRunner;

namespace VBoxDude.VM
{
    internal class VirtualMachineImporter : IVirtualMachineImporter
    {
        private IConfiguration _config;
        private IFileSystem _fileSystem;
        private IProcessRunner _runner;
        private VirtualMachineFactory _vmFactory;

        public VirtualMachineImporter(
            IConfiguration config,
            IFileSystem fileSystem,
            IProcessRunner runner,
            VirtualMachineFactory vmFactory)
        {
            _config = config;
            _fileSystem = fileSystem;
            _runner = runner;
            _vmFactory = vmFactory;
        }

        public VirtualMachine Import(string filePath, string newMachineName)
        {
            if (string.IsNullOrWhiteSpace(newMachineName))
            {
                throw new ArgumentNullException(nameof(newMachineName));
            }
            if (!_fileSystem.FileExists(filePath))
            {
                throw new ArgumentException("File doesn't exist: " + filePath);
            }

            _runner.RunAndWait(
                _config.VirtualBoxManagerApp,
                $"import {filePath} --vsys 0 {newMachineName}");

            var vm = _vmFactory.CreateFromName(newMachineName);
            return vm;
        }
    }
}