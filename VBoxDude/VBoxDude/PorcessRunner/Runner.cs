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
        public async Task<IResult> RunAsync(string appPath, string arguments)
        {
            var result = await RunAndContinue(appPath, arguments);
            if (result.ExitCode != 0)
            {
                var msg = new StringBuilder();
                msg.Append("Process failed - exit code: ");
                msg.Append(result.ExitCode);
                msg.Append(", details:");
                msg.Append(Environment.NewLine);
                msg.Append("Application path: ");
                msg.Append(appPath);
                msg.Append(Environment.NewLine);
                msg.Append("Application arguments: ");
                msg.Append(arguments);
                msg.Append(Environment.NewLine);
                msg.Append("ERROR OUTPUT: ");
                msg.Append(string.Join(Environment.NewLine, result.StandardError));
                msg.Append(Environment.NewLine);
                msg.Append("STANDARD OUTPUT: ");
                msg.Append(string.Join(Environment.NewLine, result.StandardOutput));
                throw new Exception(msg.ToString());
            }
            return result;
        }

        public Task<IResult> RunAndContinue(string appPath, string arguments)
        {
            var task = ProcessEx.RunAsync(new ProcessStartInfo()
            {
                FileName = appPath,
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false
            }).ContinueWith(new Func<Task<ProcessResults>, IResult>(t =>
            {
                ProcessResults procResult = t.Result;
                return new Result
                {
                    ExitCode = procResult.ExitCode,
                    StandardError = procResult.StandardError,
                    StandardOutput = procResult.StandardOutput
                };
            }));
            return task;
        }

        public IResult RunAndWait(string appPath, string arguments)
        {
            var task = RunAsync(appPath, arguments);
            var result = task.Result;
            return result;
        }
    }
}
