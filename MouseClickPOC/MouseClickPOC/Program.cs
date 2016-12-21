using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualBox;

namespace MouseClickPOC
{
    public class Program
    {
        private const int SleepTime = 5 * 1000; // 5s

        public static void Main(string[] args)
        {
            var name = args[0];
            Console.WriteLine("Macine name: " + name);

            IVirtualBox vBox = new VirtualBox.VirtualBox();
            var machine = vBox.FindMachine(name);

            Console.WriteLine("Wainting for machine to run...");
            while (machine.State != MachineState.MachineState_Running)
            {
                Console.WriteLine($"Machine state: {machine.State}, waiting...");
                Thread.Sleep(SleepTime);
            }
            Console.WriteLine($"Machine is running - state: {machine.State}.");

            var session = new Session();
            machine.LockMachine(session, LockType.LockType_Shared);

            var console = session.Console;
            var mouse = console.Mouse;

            Console.WriteLine("Clicking and waiting for shutdown...");

            while (machine.State == MachineState.MachineState_Running)
            {
                try
                {
                    mouse.PutMouseEventAbsolute(625, 440, 0, 0, 1);
                    Thread.Sleep(500);
                    mouse.PutMouseEventAbsolute(625, 440, 0, 0, 0);
                    Thread.Sleep(SleepTime);
                    Console.WriteLine("Clicked.");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Click failed: " + ex.ToString());
                }
            }

            Console.WriteLine($"Done - machine state: {machine.State}."); 
        }
    }
}
