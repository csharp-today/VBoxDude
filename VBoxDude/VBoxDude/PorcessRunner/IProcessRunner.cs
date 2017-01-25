using RunProcessAsTask;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VBoxDude.PorcessRunner
{
    internal interface IProcessRunner
    {
        Task<IResult> RunAsync(string appPath, string arguments);
        Task<IResult> RunAndContinue(string appPath, string arguments);
        IResult RunAndWait(string appPath, string arguments);
    }
}