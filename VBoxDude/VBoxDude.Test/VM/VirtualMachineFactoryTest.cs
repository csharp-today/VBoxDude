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

        [TestMethod]
        public void Import_VM_From_File()
        {
            // Arrange
            var test = new TestContainer();

            const string FilePath = "file-path", Name = "a-name";
            var expectedVirtualMachine = new VirtualMachine(null, null, null, null, Name);
            test.VirtualMachineImporter.Setup(m => m.Import(FilePath, Name))
                .Returns(expectedVirtualMachine);

            var factory = test.RegisterAll().Resolve<VirtualMachineFactory>();

            // Act
            VirtualMachine vm = factory.ImportFromFile(FilePath, Name);

            // Assert
            Assert.AreEqual(expectedVirtualMachine, vm);
        }
    }
}
