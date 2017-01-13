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
        public Process RunAndContinue(string appPath, string arguments)
        {
            var proc = Process.Start(
                new ProcessStartInfo()
                {
                    FileName = appPath,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            return proc;
        }

        public void RunAndWait(string appPath, string arguments)
        {
            using (var proc = RunAndContinue(appPath, arguments))
            {
                proc.WaitForExit();
                if (proc.ExitCode != 0)
                {
                    throw new Exception("Process failed - exit code: " + proc.ExitCode);
                }
            }
        }
    }
}
