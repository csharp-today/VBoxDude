using Microsoft.Practices.Unity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.PorcessRunner;
using VBoxDude.Config;

namespace VBoxDude.Test
{
    internal class TestContainer : UnityContainer
    {
        public Mock<IConfiguration> Config { get; } = new Mock<IConfiguration>();
        public Mock<IProcessRunner> ProcessRunner { get; } = new Mock<IProcessRunner>();
        
        public TestContainer RegisterAll()
        {
            this.RegisterInstance(Config.Object);
            this.RegisterInstance(ProcessRunner.Object);
            return this;
        }
    }
}
