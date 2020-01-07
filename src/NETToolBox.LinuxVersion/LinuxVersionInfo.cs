using System;
using System.Collections.Generic;
using System.Text;

namespace NETToolBox.LinuxVersion
{
    public class LinuxVersionInfo
    {
        internal LinuxVersionInfo(string kernelVersion,string osRelease,string? osVersion)
        {           
            OS_RELEASE = osRelease;
            KernelVersion = kernelVersion;
            VersionString = string.Empty;
            ParseOSRelease(osRelease,osVersion);
        }

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1056 // Uri properties should not be strings

        public string? PRETTY_NAME { get; private set; }
        public string? NAME { get; private set; }
        public string? VERSION_ID { get; private set; }
        public string? VERSION_CODENAME { get; private set; }
        public string? ID { get; private set; }
        public string? HOME_URL { get; private set; }
        public string? SUPPORT_URL { get; private set; }
        public string? BUG_REPORT_URL { get; private set; }
        public string? VERSION { get; private set; }
        public string OS_RELEASE { get; private set; }
        public string KernelVersion { get; private set; }
        public string VersionString { get; private set; }

#pragma warning restore CA1056 // Uri properties should not be strings
#pragma warning restore CA1707 // Identifiers should not contain underscores

        private void ParseOSRelease(string osRelease,string? osVersion)
        {
            var lines = osRelease.Split('\n');
            foreach (var line in lines)
            {
                if (line.StartsWith("PRETTY_NAME", StringComparison.InvariantCultureIgnoreCase)) PRETTY_NAME = GetValue(line);
                else if (line.StartsWith("NAME", StringComparison.InvariantCultureIgnoreCase)) NAME = GetValue(line);
                else if (line.StartsWith("VERSION_ID", StringComparison.InvariantCultureIgnoreCase)) VERSION_ID = GetValue(line);
                else if (line.StartsWith("VERSION_CODENAME", StringComparison.InvariantCultureIgnoreCase)) VERSION_CODENAME = GetValue(line);
                else if (line.StartsWith("ID=", StringComparison.InvariantCultureIgnoreCase)) ID = GetValue(line);
                else if (line.StartsWith("HOME_URL", StringComparison.InvariantCultureIgnoreCase)) HOME_URL = GetValue(line);
                else if (line.StartsWith("SUPPORT_URL", StringComparison.InvariantCultureIgnoreCase)) SUPPORT_URL = GetValue(line);
                else if (line.StartsWith("BUG_REPORT_URL", StringComparison.InvariantCultureIgnoreCase)) BUG_REPORT_URL = GetValue(line);               
            }
            if (ID == "ubuntu")
            {
                VersionString = $"{PRETTY_NAME}";
            }
            else if (osVersion == null)
            {
                VersionString = $"{ID} {VERSION_ID}";
            }
            else
            {
                VersionString = $"{ID} {osVersion.Trim()}";
            }
        }

        private static string GetValue(string line)
        {
            var retval = line.Substring(line.IndexOf('=',StringComparison.InvariantCultureIgnoreCase)+1);
            retval = retval.Replace("\"", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            retval = retval.Trim();
            return retval;
        }
    }
}
