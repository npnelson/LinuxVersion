using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace NETToolBox.LinuxVersion.Tests
{
    public class GetLinuxVersionTests
    {
        [Fact]
        public void AlpineTest()
        {
            var osString = File.ReadAllText("alpine_latest__etc_os-release");
            var linuxVersion = new LinuxVersionInfo("testKernel", osString);

            linuxVersion.KernelVersion.Should().Be("testKernel");
            linuxVersion.OS_RELEASE.Should().Be(osString);
            linuxVersion.NAME.Should().Be("Alpine Linux");
            linuxVersion.ID.Should().Be("alpine");
            linuxVersion.VERSION_ID.Should().Be("3.11.2");
            linuxVersion.BUG_REPORT_URL.Should().Be("https://bugs.alpinelinux.org/");
            linuxVersion.HOME_URL.Should().Be("https://alpinelinux.org/");
            linuxVersion.PRETTY_NAME.Should().Be("Alpine Linux v3.11");
            linuxVersion.VERSION_CODENAME.Should().BeNull();
            linuxVersion.VersionString.Should().Be("alpine 3.11.2");
        }
    }
}
