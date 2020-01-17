using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
[assembly: InternalsVisibleTo("NETToolBox.LinuxVersion.Tests")]
namespace NETToolBox.LinuxVersion
{
    public static class GetLinuxVersion
    {
        static LinuxVersionInfo? _versioninfo;
        public static LinuxVersionInfo GetLinuxVersionInfo()
        {
            if (_versioninfo != null) return _versioninfo; //no need to run the commands on every call, so we will only run the first time and cache the result
            else
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) throw new InvalidOperationException("Do not call GetLinuxVersionInfo when not running on Linux");
                var kernel = RuntimeInformation.OSDescription;
                var osReleaseOutput = GetCommandOutput("/bin/cat", "/etc/os-release");
                string? osVersion = null;
                if (osReleaseOutput.Contains("debian", StringComparison.OrdinalIgnoreCase))
                {
                    osVersion = GetCommandOutput("/bin/cat", "/etc/debian_version");
                }
                var retval = new LinuxVersionInfo(kernel, osReleaseOutput, osVersion);
                _versioninfo = retval;
                return retval;
            }
        }

        private static string GetCommandOutput(string command, string arguments)
        {
            string retval;
            System.Diagnostics.ProcessStartInfo procStartInfo;
            procStartInfo = new System.Diagnostics.ProcessStartInfo(command, arguments);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                retval = proc.StandardOutput.ReadToEnd();
            }
            return retval;
        }
    }
}
