using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using VBoxDude.PorcessRunner;

namespace VBoxDude.Test.ProcessRunner
{
    [TestClass]
    public class ProcessRunnerTest
    {
        private const string WaitTwoSecApp = "cmd.exe";
        private const string WaitTwoSecArgs = "/c ping 127.0.0.1 -n 3 > nul";

        [TestMethod]
        public void Run_Should_Continue()
        {
            // Arrange
            var runner = new TestContainer().RegisterAll().Resolve<Runner>();

            // Act
            var startTime = DateTime.Now;
            runner.RunAndContinue(WaitTwoSecApp, WaitTwoSecArgs);
            var endTime = DateTime.Now;

            // Assert
            var time = endTime - startTime;
            Assert.IsTrue(time.TotalSeconds < 2);
        }

        [TestMethod]
        public void Run_Should_Wait_For_Exit()
        {
            // Arrange
            var runner = new TestContainer().RegisterAll().Resolve<Runner>();

            // Act
            var startTime = DateTime.Now;
            runner.RunAndWait(WaitTwoSecApp, WaitTwoSecArgs);
            var endTime = DateTime.Now;

            // Assert
            var time = endTime - startTime;
            Assert.IsTrue(time.TotalSeconds > 2);
        }
    }
}
