using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBoxDude.Config;
using VBoxDude.PorcessRunner;

namespace VBoxDude.VM.Disks
{
    internal class DiskPathGetter : IDiskPathGetter
    {
        private IConfiguration _config;
        private IProcessRunner _runner;

        public DiskPathGetter(
            IConfiguration config,
            IProcessRunner runner)
        {
            _config = config;
            _runner = runner;
        }

        public async Task<IEnumerable<string>> GetDiskPathAsync(string machineName)
        {
            var args = $"showvminfo {machineName} --machinereadable";
            var result = await _runner.RunAsync(_config.VirtualBoxManagerApp, args);

            if (result == null || result.StandardOutput == null)
            {
                return new string[] { };
            }

            var pathLines = result.StandardOutput.Where(l =>
            {
                var lower = l.ToLower();
                bool isDiskPath = lower.Contains(".vmdk") || lower.Contains(".vhd");
                return isDiskPath && l.Contains("IDE Controller");
            });

            var pathes = pathLines.Select(l => l.Split('=')[1].Trim('"', ' ', '\t', '\r'));
            return pathes;
        }
    }
}