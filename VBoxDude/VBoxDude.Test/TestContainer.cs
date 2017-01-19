using Microsoft.Practices.Unity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.PorcessRunner;
using VBoxDude.Config;
using VBoxDude.FileSystem;
using VBoxDude.VM;

namespace VBoxDude.Test
{
    internal class TestContainer : UnityContainer
    {
        public Mock<IConfiguration> Config { get; } = new Mock<IConfiguration>();
        public Mock<IFileSystem> FileSystem { get; } = new Mock<IFileSystem>();
        public Mock<IProcessRunner> ProcessRunner { get; } = new Mock<IProcessRunner>();
        public Mock<IVirtualMachineImporter> VirtualMachineImporter { get; } = new Mock<IVirtualMachineImporter>();

        public TestContainer RegisterAll()
        {
            this.RegisterInstance(Config.Object);
            this.RegisterInstance(FileSystem.Object);
            this.RegisterInstance(ProcessRunner.Object);
            this.RegisterInstance<IUnityContainer>(this);
            this.RegisterInstance(VirtualMachineImporter.Object);
            return this;
        }
    }
}
