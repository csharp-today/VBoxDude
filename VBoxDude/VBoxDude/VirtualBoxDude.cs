using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.VM;
using Microsoft.Practices.Unity;

namespace VBoxDude
{
    public class VirtualBoxDude
    {
        private GlobalContainer _container = new GlobalContainer();
        private VirtualMachineFactory _vmFactory;

        public VirtualMachineFactory VMFactory
        {
            get
            {
                if (_vmFactory == null)
                {
                    _vmFactory = _container.Resolve<VirtualMachineFactory>();
                }
                return _vmFactory;
            }
        }
    }
}
