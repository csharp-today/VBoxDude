using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.FileSystem;
using VBoxDude.PorcessRunner;
using VBoxDude.VM;
using VBoxDude.VM.Disks;

namespace VBoxDude.Test.Config
{
    [TestClass]
    public class GlobalContainerTest
    {
        [TestMethod]
        public void Resolve_IConfiguration()
        {
            TestResolve<IConfiguration>();
        }

        [TestMethod]
        public void Resolve_IDiskPathGetter()
        {
            TestResolve<IDiskPathGetter>();
        }

        [TestMethod]
        public void Resolve_IFileSystem()
        {
            TestResolve<IFileSystem>();
        }

        [TestMethod]
        public void Resolve_IProcessRunner()
        {
            TestResolve<IProcessRunner>();
        }

        [TestMethod]
        public void Resolve_VirtualMachineFactory()
        {
            TestResolve<VirtualMachineFactory>();
        }

        [TestMethod]
        public void Resolve_IVirtualMachineImporter()
        {
            TestResolve<IVirtualMachineImporter>();
        }

        private void TestResolve<T>()
        {
            // Assert
            var container = new GlobalContainer();

            // Act
            var obj = container.Resolve<T>();

            // Assert
            Assert.IsNotNull(obj);
        }
    }
}
