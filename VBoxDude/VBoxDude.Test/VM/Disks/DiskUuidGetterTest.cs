using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBoxDude.VM.Disks;
using Microsoft.Practices.Unity;
using Moq;
using VBoxDude.PorcessRunner;

namespace VBoxDude.Test.VM.Disks
{
    [TestClass]
    public class DiskUuidGetterTest
    {
        [TestMethod]
        public async Task Execute_VBoxManage()
        {
            // Arrange
            var test = GenerateContainer();

            const string AppPath = @"some\path\to\app.exe";
            test.Config.Setup(m => m.VirtualBoxManagerApp).Returns(AppPath);

            string appArgs = null;
            test.ProcessRunner.Setup(m => m.RunAsync(AppPath, It.IsAny<string>()))
                .Returns(new Func<string, string, Task<IResult>>((path, args) =>
                {
                    appArgs = args;
                    return Task.FromResult((IResult)new Result { ExitCode = 0 });
                }));

            var getter = test.RegisterAll().Resolve<DiskUuidGetter>();
            const string FilePath = @"some\file\path.vhd";

            // Act
            await getter.GetDiskUuidAsync(FilePath);

            // Assert
            Assert.AreEqual($"showhdinfo \"{FilePath}\"", appArgs);
        }

        [TestMethod]
        public async Task Return_DiskUuid()
        {
            // Arrange
            var test = GenerateContainer();

            const string ExpectedUuid = "someUUID";
            test.ProcessRunner.Setup(m => m.RunAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult((IResult)new Result
                {
                    ExitCode = 0,
                    StandardOutput = new[] { DiskUuidGetter.UuidIdentifier + " " + ExpectedUuid }
                }));

            var getter = test.RegisterAll().Resolve<DiskUuidGetter>();

            // Act
            string uuid = await getter.GetDiskUuidAsync(string.Empty);

            // Assert
            Assert.AreEqual(ExpectedUuid, uuid);
        }

        [TestMethod]
        public async Task Throw_ArgumentEx_When_File_DoesNot_Exist()
        {
            // Arrange
            var test = new TestContainer();

            const string FilePath = @"some\file\path.vhd";
            test.FileSystem.Setup(m => m.FileExists(It.IsAny<string>())).Returns(true);
            test.FileSystem.Setup(m => m.FileExists(FilePath)).Returns(false);

            IDiskUuidGetter getter = test.RegisterAll().Resolve<DiskUuidGetter>();

            // Act
            var ex = await TestAssistant.CatchExceptionAsync(getter.GetDiskUuidAsync(FilePath));

            // Assert
            Assert.IsNotNull(ex);
            Assert.IsTrue(ex is ArgumentException);
        }

        private TestContainer GenerateContainer()
        {
            var test = new TestContainer();
            test.FileSystem.Setup(m => m.FileExists(It.IsAny<string>())).Returns(true);
            return test;
        }
    }
}
