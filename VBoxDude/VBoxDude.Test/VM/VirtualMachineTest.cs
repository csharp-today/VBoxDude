using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VBoxDude.VM;
using VBoxDude.PorcessRunner;
using Microsoft.Practices.Unity;

namespace VBoxDude.Test.VM
{
    [TestClass]
    public class VirtualMachineTest
    {
        [TestMethod]
        public void Should_Have_ProcessRunner()
        {
            // Arrange
            var f = new TestContainer().RegisterAll().Resolve<VirtualMachineFactory>();
            var vm = f.CreateFromName("");

            // Act
            var runner = vm.ProcessRunner;

            // Assert
            Assert.IsTrue(runner is IProcessRunner);
            Assert.IsNotNull(runner);
        }

        [TestMethod]
        public void Start_Should_Call_ProcessRunner()
        {
            // Arrange
            var test = new TestContainer();

            const string AppName = "someapp.exe";
            test.Config.Setup(m => m.VirtualBoxHeadlessApp).Returns(AppName);

            const string MachineName = "somename";
            bool runnerCalled = false;
            test.ProcessRunner.Setup(m => m.RunAndContinue(AppName, "-s " + MachineName))
                .Callback(() => runnerCalled = true);

            var f = test.RegisterAll().Resolve<VirtualMachineFactory>();
            var vm = f.CreateFromName(MachineName);

            // Act
            vm.Start();

            // Assert
            Assert.IsTrue(runnerCalled);
        }
    }
}
