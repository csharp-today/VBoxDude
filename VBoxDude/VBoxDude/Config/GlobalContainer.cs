using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.PorcessRunner;

namespace VBoxDude.Config
{
    class GlobalContainer : UnityContainer
    {
        public GlobalContainer()
        {
            this.RegisterType<IConfiguration, DefaultConfiguration>();
            this.RegisterType<IProcessRunner, Runner>();
        }
    }
}
