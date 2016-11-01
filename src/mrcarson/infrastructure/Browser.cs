using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace mrcarson.infrastructure
{
    public static class Browser
    {
        public static void Open(string url) {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                Process.Start("open", url);
            }
            else {
                throw new PlatformNotSupportedException();
            }
        }
    }
}