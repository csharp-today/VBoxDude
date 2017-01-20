using RunProcessAsTask;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VBoxDude.PorcessRunner
{
    public interface IProcessRunner
    {
        Task RunAsync(string appPath, string arguments);
        Task<ProcessResults> RunAndContinue(string appPath, string arguments);
        void RunAndWait(string appPath, string arguments);
    }
}