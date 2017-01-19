using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBoxDude.Test
{
    internal static class TestAssistant
    {
        public static Exception CatchException(Action act)
        {
            try
            {
                act();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
