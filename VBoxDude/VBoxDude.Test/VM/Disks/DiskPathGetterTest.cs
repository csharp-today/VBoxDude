using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.VM.Disks;
using Microsoft.Practices.Unity;
using RunProcessAsTask;
using System.Diagnostics;
using VBoxDude.PorcessRunner;

namespace VBoxDude.Test.VM.Disks
{
    [TestClass]
    public class DiskPathGetterTest
    {
        [TestMethod]
        public async Task Return_Nothing_If_No_Disks()
        {
            // Arrange
            var test = new TestContainer();
            MockProcessRunnerOutput(test, @"hardwareuuid=""7b7ed744 - 7386 - 4200 - b361 - eaff538a455b""
memory = 4096
pagefusion = ""off""
vram = 128
cpuexecutioncap = 100
hpet = ""off""
chipset = ""piix3""
firmware = ""BIOS""");

            var getter = test.RegisterAll().Resolve<DiskPathGetter>();

            // Act
            var pathes = await getter.GetDiskPathsAsync("some-name");

            // Assert
            Assert.IsNotNull(pathes);
            Assert.AreEqual(0, pathes.Count());
        }

        [TestMethod]
        public async Task Return_Two_Pathes()
        {
            // Arrange
            var test = new TestContainer();

            const string ExpectedPath1 = @"C:\some-path\Disk-image.vhd";
            const string ExpectedPath2 = @"C:\some-path\Disk-image.vmdk";
            MockProcessRunnerOutput(test, @"storagecontrollerportcount0=""2""
storagecontrollerbootable0 = ""on""
""IDE Controller-0-0"" = """ + ExpectedPath1 + @"""
""IDE Controller-1-0"" = """ + ExpectedPath2 + "\"");

            var getter = test.RegisterAll().Resolve<DiskPathGetter>();

            // Act
            var pathes = await getter.GetDiskPathsAsync("some-name");

            // Assert
            Assert.IsNotNull(pathes);
            Assert.AreEqual(2, pathes.Count());
            Assert.IsTrue(pathes.Contains(ExpectedPath1));
            Assert.IsTrue(pathes.Contains(ExpectedPath2));
        }

        [TestMethod]
        public async Task Return_Vhd_Path()
        {
            // AAA
            await TestSinglePathReturn(@"C:\some-path\Disk-image.vhd");
        }

        [TestMethod]
        public async Task Return_Vmdk_Path()
        {
            // AAA
            await TestSinglePathReturn(@"C:\some-path\Disk-image.vmdk");
        }

        [TestMethod]
        public async Task Should_Call_VBoxManager()
        {
            // Arrange
            var test = new TestContainer();

            const string ManagerPath = "some-manager.exe";
            test.Config.Setup(m => m.VirtualBoxManagerApp).Returns(ManagerPath);

            string appPath = null, appArgs = null;
            test.ProcessRunner.Setup(m => m.RunAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Func<string, string, Task<IResult>>((app, args) =>
                {
                    appPath = app;
                    appArgs = args;
                    return Task.FromResult<IResult>(null);
                }));

            IDiskPathGetter getter = test.RegisterAll().Resolve<DiskPathGetter>();
            const string ExpectedMachineName = "some-name";

            // Act
            await getter.GetDiskPathsAsync(ExpectedMachineName);

            // Assert
            Assert.AreEqual(ManagerPath, appPath);
            Assert.IsNotNull(appArgs);
            Assert.IsTrue(appArgs.StartsWith("showvminfo " + ExpectedMachineName));
            Assert.IsTrue(appArgs.EndsWith("--machinereadable"));
        }

        private void MockProcessRunnerOutput(TestContainer test, string output)
        {
            test.ProcessRunner.Setup(m => m.RunAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult<IResult>(new Result
                {
                    ExitCode = 0,
                    StandardOutput = output.Split('\n')
                }));
        }

        private async Task TestSinglePathReturn(string expectedPath)
        {
            // Arrange
            var test = new TestContainer();

            MockProcessRunnerOutput(test, @"storagecontrollerportcount0=""2""
storagecontrollerbootable0 = ""on""
""IDE Controller-0-0"" = """ + expectedPath + "\"");

            var getter = test.RegisterAll().Resolve<DiskPathGetter>();

            // Act
            var pathes = await getter.GetDiskPathsAsync("some-name");

            // Assert
            Assert.IsNotNull(pathes);
            Assert.AreEqual(1, pathes.Count());
            Assert.AreEqual(expectedPath, pathes.First());
        }
    }
}
