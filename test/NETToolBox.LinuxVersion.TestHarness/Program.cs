using System;

namespace NETToolBox.LinuxVersion.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetLinuxVersion.GetLinuxVersionInfo().VersionString);
        }
    }
}
