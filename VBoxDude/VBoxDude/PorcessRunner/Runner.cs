using RunProcessAsTask;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBoxDude.PorcessRunner
{
    internal class Runner : IProcessRunner
    {
        public async Task RunAsync(string appPath, string arguments)
        {
            var result = await RunAndContinue(appPath, arguments);
            if (result.ExitCode != 0)
            {
                var msg = new StringBuilder();
                msg.Append("Process failed - exit code: ");
                msg.Append(result.ExitCode);
                throw new Exception(msg.ToString());
            }
        }

        public Task<ProcessResults> RunAndContinue(string appPath, string arguments)
        {
            var task = ProcessEx.RunAsync(new ProcessStartInfo()
                {
                    FileName = appPath,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            return task;
        }

        public void RunAndWait(string appPath, string arguments)
        {
            var task = RunAsync(appPath, arguments);
            task.Wait();
        }
    }
}
