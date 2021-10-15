using Microsoft.Win32;
using System.Windows.Forms;

namespace SymlinkSwapper.Logic
{
    public static class AutostartHelper
    {
        private static readonly RegistryKey autostartRegistryKey =
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private static readonly string executablePath = Application.ExecutablePath;

        private static readonly string registryKeyName = "SymlinkSwapper";

        public static bool RunsOnSystemStartup =>
            autostartRegistryKey.GetValue(registryKeyName) != null;

        public static bool SetAppAutostart(bool shouldStartWithSystem)
        {
            if (shouldStartWithSystem)
            {
                autostartRegistryKey.SetValue(registryKeyName, executablePath);
            }

            else if (RunsOnSystemStartup)
            {
                autostartRegistryKey.DeleteValue(registryKeyName);
            }

            return RunsOnSystemStartup;
        }

    }
}
