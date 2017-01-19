using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.FileSystem;
using VBoxDude.PorcessRunner;
using VBoxDude.VM;

namespace VBoxDude.Config
{
    class GlobalContainer : UnityContainer
    {
        public GlobalContainer()
        {
            this.RegisterType<IConfiguration, DefaultConfiguration>();
            this.RegisterType<IFileSystem, FileSystem.FileSystem>();
            this.RegisterType<IProcessRunner, Runner>();
            this.RegisterInstance<IUnityContainer>(this);
            this.RegisterType<IVirtualMachineImporter, VirtualMachineImporter>();
        }
    }
}
