using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.FileSystem;
using VBoxDude.PorcessRunner;
using VBoxDude.VM;
using VBoxDude.VM.Disks;

namespace VBoxDude.Config
{
    internal class GlobalContainer : UnityContainer
    {
        public GlobalContainer()
        {
            this.RegisterType<IConfiguration, DefaultConfiguration>();
            this.RegisterType<IDiskPathGetter, DiskPathGetter>();
            this.RegisterType<IFileSystem, FileSystem.FileSystem>();
            this.RegisterType<IProcessRunner, Runner>();

            this.RegisterType<VirtualMachineFactory>(new InjectionFactory(c =>
            {
                return new VirtualMachineFactory(
                    c.Resolve<IConfiguration>(),
                    c.Resolve<IUnityContainer>(),
                    c.Resolve<IProcessRunner>(),
                    c.Resolve<IDiskPathGetter>());
            }));
            this.RegisterType<IVirtualMachineImporter>(new InjectionFactory(c =>
            {
                return new VirtualMachineImporter(
                    c.Resolve<IConfiguration>(),
                    c.Resolve<IFileSystem>(),
                    c.Resolve<IProcessRunner>(),
                    c.Resolve<VirtualMachineFactory>());
            }));
        }
    }
}
