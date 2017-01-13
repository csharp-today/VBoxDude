using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.VM;
using Microsoft.Practices.Unity;

namespace VBoxDude.Test.VM
{
    [TestClass]
    public class VirtualMachineFactoryTest
    {
        [TestMethod]
        public void Create_VM_From_Name()
        {
            // Arrange
            var f = new TestContainer().RegisterAll().Resolve<VirtualMachineFactory>();
            const string Name = "testName";

            // Act
            VirtualMachine vm = f.CreateFromName(Name);

            // Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual(Name, vm.Name);
        }
    }
}
