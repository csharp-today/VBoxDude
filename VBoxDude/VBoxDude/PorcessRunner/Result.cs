using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBoxDude.PorcessRunner
{
    internal class Result : IResult
    {
        public int ExitCode { get; set; }
        public string[] StandardError { get; set; }
        public string[] StandardOutput { get; set; }
    }
}
