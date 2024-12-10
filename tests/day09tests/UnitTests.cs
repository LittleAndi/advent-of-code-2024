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

    [Fact]
    public void Test3()
    {
        var drive = new Drive("2333133121414131402");
        drive.PackEntireFiles();
        var i = 0;

        // 00 992 111 777 . 44. 333 . .. . 5555 . 6666 . ... . 8888  ..
        drive.Disk[i++].ShouldBe([0, 0]);
        drive.Disk[i++].ShouldBe([9, 9, 2]);
        drive.Disk[i++].ShouldBe([1, 1, 1]);
        drive.Disk[i++].ShouldBe([7, 7, 7]);
        drive.Disk[i++].ShouldBe([-1]);
        drive.Disk[i++].ShouldBe([4, 4, -1]);
        drive.Disk[i++].ShouldBe([3, 3, 3]);
        drive.Disk[i++].ShouldBe([-1]);
        drive.Disk[i++].ShouldBe([-1, -1]);
        drive.Disk[i++].ShouldBe([-1]);
        drive.Disk[i++].ShouldBe([5, 5, 5, 5]);
        drive.Disk[i++].ShouldBe([-1]);
        drive.Disk[i++].ShouldBe([6, 6, 6, 6]);
        drive.Disk[i++].ShouldBe([-1]);
        drive.Disk[i++].ShouldBe([-1, -1, -1]);
        drive.Disk[i++].ShouldBe([-1]);
        drive.Disk[i++].ShouldBe([8, 8, 8, 8]);
        drive.Disk[i++].ShouldBe([]);
        drive.Disk[i++].ShouldBe([-1, -1]);
    }

    [Fact]
    public void Test4()
    {
        var drive = new Drive("2333133121414131402");
        drive.PackEntireFiles();
        drive.FilesystemChecksum().ShouldBe(2858);
    }
}
