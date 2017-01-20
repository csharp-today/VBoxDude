using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    public class VirtualMachineImporterTest
    {
        [TestMethod]
        public void Execute_VirtualBoxManager()
        {
            // Arrange
            var test = GenerateTestContainer();

            test.Config.Setup(m => m.VirtualBoxManagerApp).Returns("some-manager.exe");

            bool executeManager = false;
            test.ProcessRunner.Setup(m => m.RunAndWait(test.Config.Object.VirtualBoxManagerApp, It.IsAny<string>()))
                .Callback(() => executeManager = true);

            var importer = test.RegisterAll().Resolve<VirtualMachineImporter>();

            // Act
            importer.Import("some-path", "some-name");

            // Assert
            Assert.IsTrue(executeManager);
        }

        [TestMethod]
        public void Fail_If_FilePath_DoesNot_Exist()
        {
            // Arrange
            var test = new TestContainer();

            const string FilePath = "file-path";
            test.FileSystem.Setup(m => m.FileExists(FilePath))
                .Returns(false);

            var importer = test.RegisterAll().Resolve<VirtualMachineImporter>();

            // Act
            var ex = TestAssistant.CatchException(() => importer.Import(FilePath, "some-name"));

            // Assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Import_Command_Has_Default_VirtualSystem_Parameter()
        {
            // Arrange
            var test = GenerateTestContainer();

            string args = null;
            test.ProcessRunner.Setup(m => m.RunAndWait(It.IsAny<string>(), It.IsAny<string>()))
                .Callback(new Action<string, string>((appPath, appArgs) => args = appArgs));

            var importer = test.RegisterAll().Resolve<VirtualMachineImporter>();

            // Act
            importer.Import("some-path", "some-name");

            // Assert
            Assert.IsNotNull(args);
            Assert.IsTrue(args.Contains("--vsys 0"));
        }

        [TestMethod]
        public void Import_Command_With_FilePath()
        {
            // Arrange
            var test = GenerateTestContainer();

            string args = null;
            test.ProcessRunner.Setup(m => m.RunAndWait(It.IsAny<string>(), It.IsAny<string>()))
                .Callback(new Action<string, string>((appPath, appArgs) => args = appArgs));

            var importer = test.RegisterAll().Resolve<VirtualMachineImporter>();

            const string AppPath = "expected=app.exe";
            const string ExpectedArgs = "import \"" + AppPath + "\"";

            // Act
            importer.Import(AppPath, "some-name");

            // Assert
            Assert.IsNotNull(args);
            Assert.IsTrue(args.StartsWith(ExpectedArgs));
        }

        [TestMethod]
        public void Import_Command_With_MachineName()
        {
            // Arrange
            var test = GenerateTestContainer();

            string args = null;
            test.ProcessRunner.Setup(m => m.RunAndWait(It.IsAny<string>(), It.IsAny<string>()))
                .Callback(new Action<string, string>((appPath, appArgs) => args = appArgs));

            var importer = test.RegisterAll().Resolve<VirtualMachineImporter>();

            const string ExpectedMachineName = "new-machine-name";

            // Act
            importer.Import("some-path", ExpectedMachineName);

            // Assert
            Assert.IsNotNull(args);
            Assert.IsTrue(args.EndsWith(ExpectedMachineName));
        }

        [TestMethod]
        public void Implement_IVirtualMachineImporter()
        {
            // Arrange
            var importer = new TestContainer().RegisterAll().Resolve<VirtualMachineImporter>();

            // Act
            var implementInterface = importer is IVirtualMachineImporter;

            // Assert
            Assert.IsTrue(implementInterface);
        }

        [TestMethod]
        public void Name_Should_Not_Be_Empty()
        {
            // Arrange
            const string Path = "some-path";
            var importer = GenerateTestContainer().RegisterAll().Resolve<VirtualMachineImporter>();

            // Act
            var validCase = TestAssistant.CatchException(() => importer.Import(Path, "some-name"));
            var invalid1 = TestAssistant.CatchException(() => importer.Import(Path, "   "));
            var invalid2 = TestAssistant.CatchException(() => importer.Import(Path, string.Empty));
            var invalid3 = TestAssistant.CatchException(() => importer.Import(Path, null));

            // Assert
            Assert.IsNull(validCase);
            Assert.IsNotNull(invalid1);
            Assert.IsNotNull(invalid2);
            Assert.IsNotNull(invalid3);
        }

        [TestMethod]
        public void Return_VirtualMachine_With_TheSame_Name()
        {
            // Arrange
            const string ExpectedName = "vm-name";
            var importer = GenerateTestContainer().RegisterAll().Resolve<VirtualMachineImporter>();

            // Act
            VirtualMachine vm = importer.Import("some-path", ExpectedName);

            // Assert
            Assert.IsNotNull(vm);
            Assert.AreEqual(ExpectedName, vm.Name);
        }

        private TestContainer GenerateTestContainer()
        {
            var test = new TestContainer();
            test.FileSystem.Setup(m => m.FileExists(It.IsAny<string>()))
                .Returns(true);
            return test;
        }
    }
}
