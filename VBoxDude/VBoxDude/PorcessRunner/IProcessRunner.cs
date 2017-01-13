using System.Diagnostics;

namespace VBoxDude.PorcessRunner
{
    public interface IProcessRunner
    {
        Process RunAndContinue(string appPath, string arguments);
        void RunAndWait(string appPath, string arguments);
    }
}