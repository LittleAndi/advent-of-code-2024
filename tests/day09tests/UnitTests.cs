using Shouldly;

namespace day09tests;

public class UnitTests
{
    [Fact]
    public void Test1()
    {
        var drive = new Drive("12345");
        drive.Disk.Count.ShouldBe(5);
        drive.Pack();
        drive.Disk[0].ShouldBe([0]);
        drive.Disk[1].ShouldBe([2, 2]);
        drive.Disk[2].ShouldBe([1, 1, 1]);
        drive.Disk[3].ShouldBe([2, 2, 2, -1]);
        drive.Disk[4].ShouldBe([-1, -1, -1, -1, -1]);
    }

    [Fact]
    public void Test2()
    {
        var drive = new Drive("2333133121414131402");
        drive.Pack();
        drive.FilesystemChecksum().ShouldBe(1928);
    }
}
