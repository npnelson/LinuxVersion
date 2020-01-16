using FluentAssertions;
using System.Runtime.InteropServices;
using Xunit;

namespace NETToolBox.LinuxVersion.Tests
{
    public class GetLinuxVersionTests
    {
        [Fact]
        public void LinuxTest()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                GetLinuxVersion.GetLinuxVersionInfo().VersionString.Should().Be("ubuntu 18.04", "note this test will only pass if running on ubuntu 18.04");
            }
        }
    }
}
