using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBoxDude.PorcessRunner
{
    internal interface IResult
    {
        int ExitCode { get; }
        string[] StandardError { get; }
        string[] StandardOutput { get; }
    }
}
