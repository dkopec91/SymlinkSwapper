using System.Diagnostics;
using System.Linq;

namespace SymlinkSwapper.Logic
{
    public static class SingleAppInstanceHelper
    {
        public static bool IsAppAlreadyRunning()
        {
            Process[] processes = Process.GetProcesses();
            Process currentProc = Process.GetCurrentProcess();

            return processes.Any(p => p.ProcessName == currentProc.ProcessName
                                   && p.Id != currentProc.Id);
        }
    }
}
