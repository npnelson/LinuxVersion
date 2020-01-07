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
            var linuxVersion = new LinuxVersionInfo("testKernel", osString,null);

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

        [Fact]
        public void DebianTest()
        {
            var osString = File.ReadAllText("debian_latest__etc_os-release");
            var osVersion = File.ReadAllText("debian_latest__etc_debian_version");
            var linuxVersion = new LinuxVersionInfo("testKernel", osString,osVersion);

            linuxVersion.KernelVersion.Should().Be("testKernel");
            linuxVersion.OS_RELEASE.Should().Be(osString);
            linuxVersion.NAME.Should().Be("Debian GNU/Linux");
            linuxVersion.ID.Should().Be("debian");
            linuxVersion.VERSION_ID.Should().Be("10");
            linuxVersion.BUG_REPORT_URL.Should().Be("https://bugs.debian.org/");
            linuxVersion.HOME_URL.Should().Be("https://www.debian.org/");
            linuxVersion.PRETTY_NAME.Should().Be("Debian GNU/Linux 10 (buster)");
            linuxVersion.VERSION_CODENAME.Should().Be("buster");
            linuxVersion.VersionString.Should().Be("debian 10.2");
        }

        [Fact]
        public void UbuntuTest()
        {
            var osString = File.ReadAllText("ubuntu_latest__etc_os-release");          
            var linuxVersion = new LinuxVersionInfo("testKernel", osString, null);

            linuxVersion.KernelVersion.Should().Be("testKernel");
            linuxVersion.OS_RELEASE.Should().Be(osString);
            linuxVersion.NAME.Should().Be("Ubuntu");
            linuxVersion.ID.Should().Be("ubuntu");
            linuxVersion.VERSION_ID.Should().Be("18.04");
            linuxVersion.BUG_REPORT_URL.Should().Be("https://bugs.launchpad.net/ubuntu/");
            linuxVersion.HOME_URL.Should().Be("https://www.ubuntu.com/");
            linuxVersion.PRETTY_NAME.Should().Be("Ubuntu 18.04.3 LTS");
            linuxVersion.VERSION_CODENAME.Should().Be("bionic");
            linuxVersion.VersionString.Should().Be("Ubuntu 18.04.3 LTS");
        }
    }
}
