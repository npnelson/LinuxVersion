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
                GetLinuxVersion.GetLinuxVersionInfo().VersionString.Should().Contain("Ubuntu", "note this test will only pass if running on ubuntu"); //just aiming for decent code coverage
            }
        }
    }
}
