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
using VBoxDude.VM.Disks;

namespace VBoxDude.Test
{
    internal class TestContainer : UnityContainer
    {
        public Mock<IConfiguration> Config { get; } = new Mock<IConfiguration>();
        public Mock<IDiskPathGetter> DiskPathGetter { get; } = new Mock<IDiskPathGetter>();
        public Mock<IFileSystem> FileSystem { get; } = new Mock<IFileSystem>();
        public Mock<IProcessRunner> ProcessRunner { get; } = new Mock<IProcessRunner>();
        public Mock<IVirtualMachineImporter> VirtualMachineImporter { get; } = new Mock<IVirtualMachineImporter>();

        public TestContainer RegisterAll()
        {
            this.RegisterInstance(Config.Object);
            this.RegisterInstance(DiskPathGetter.Object);
            this.RegisterInstance(FileSystem.Object);
            this.RegisterInstance(ProcessRunner.Object);
            this.RegisterInstance<IUnityContainer>(this);
            this.RegisterInstance(VirtualMachineImporter.Object);
            this.RegisterType<VirtualMachineFactory>(new InjectionFactory(c =>
            {
                return new VirtualMachineFactory(
                    c.Resolve<IConfiguration>(),
                    c.Resolve<IUnityContainer>(),
                    c.Resolve<IProcessRunner>(),
                    c.Resolve<IDiskPathGetter>());
            }));
            this.RegisterType<VirtualMachineImporter>(new InjectionFactory(c =>
            {
                return new VirtualMachineImporter(
                    c.Resolve<IConfiguration>(),
                    c.Resolve<IFileSystem>(),
                    c.Resolve<IProcessRunner>(),
                    c.Resolve<VirtualMachineFactory>());
            }));
            return this;
        }
    }
}
