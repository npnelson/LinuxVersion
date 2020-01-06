using System;
using System.IO;
using System.Linq;

namespace NETToolBox.LinuxVersion.Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteOutput("alpine:latest", "/bin/cat", "/etc/os-release");
            
        }
        public static void WriteOutput(string imagename, string command, string arguments)
        {
            var output = ExtractOutput(imagename, command, arguments);
            var fileName = $"..\\..\\..\\..\\NETToolBox.LinuxVersion.Tests\\{imagename}_{arguments}";
            fileName = fileName.Replace(':', '_');
            fileName = fileName.Replace('/', '_');
            File.WriteAllText(fileName, output);
        }
        public static string ExtractOutput(string imagename, string command, string arguments)
        {
            string retval;
            System.Diagnostics.ProcessStartInfo procStartInfo;
            var commandArguments = $"run -it --entrypoint {command} {imagename} {arguments}";
            procStartInfo = new System.Diagnostics.ProcessStartInfo("docker",commandArguments);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
             retval = proc.StandardOutput.ReadToEnd();           
            return retval;
        }

        public static string GetVersion()
        {
            string retval;
            System.Diagnostics.ProcessStartInfo procStartInfo;
            procStartInfo = new System.Diagnostics.ProcessStartInfo("/bin/cat", "/etc/os-release");
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            var result = proc.StandardOutput.ReadToEnd();
            var resultItems = result.Split('\n').Where(x => x.Contains("ID"));
            retval = string.Join(' ', resultItems.Single(x => x.StartsWith("ID=")).Substring(3), resultItems.Single(x => x.StartsWith("VERSION_ID=")).Substring(11).Replace("\\", string.Empty).Replace("\"", string.Empty));
            return retval;
        }
    }
}
