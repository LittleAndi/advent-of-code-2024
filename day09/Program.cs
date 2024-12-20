﻿using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

var line = File.ReadAllLines("input.txt")[0];

var drive = new Drive(line);
drive.Pack();
var checksum = drive.FilesystemChecksum();

System.Console.WriteLine($"Filesystem checksum when moving block by block: {checksum}");

// Part 2
drive = new Drive(line);
drive.PackEntireFiles();
checksum = drive.FilesystemChecksum();

System.Console.WriteLine($"Filesystem checksum when moving entire files: {checksum}");

public class Drive
{
    private readonly int[] diskMap;
    private readonly List<int[]> disk;

    public Drive(string diskMap)
    {
        this.diskMap = diskMap.ToArray().Select(x => (int)char.GetNumericValue(x)).ToArray();

        disk = Populate();
    }

    private List<int[]> Populate()
    {
        bool isFile = true;
        int fileId = 0;
        var disk = new List<int[]>();
        foreach (var diskMapInfo in diskMap)
        {
            if (isFile)
            {
                var file = new int[diskMapInfo];
                Array.Fill(file, fileId);
                disk.Add(file);
                fileId++;
            }
            else
            {
                var emptySpace = new int[diskMapInfo];
                Array.Fill(emptySpace, -1);
                disk.Add(emptySpace);
            }
            isFile = !isFile;
        }
        return disk;
    }

    public void Pack()
    {
        // First empty space on disk
        var firstEmptySpace = disk.First(x => x.Contains(-1));
        var firstEmptySpaceBlock = Array.FindIndex(firstEmptySpace, x => x.Equals(-1));

        // Last block of a file on disk
        var lastFile = disk.Where(x => x.Any(y => y > 0)).Last();
        var lastFileBlock = Array.FindLastIndex(lastFile, x => !x.Equals(-1));

        while (true)
        {
            // Move block
            firstEmptySpace[firstEmptySpaceBlock] = lastFile[lastFileBlock];
            lastFile[lastFileBlock] = -1;

            // First empty space on disk
            firstEmptySpace = disk.First(x => x.Contains(-1));
            firstEmptySpaceBlock = Array.FindIndex(firstEmptySpace, x => x.Equals(-1));

            // Last block of a file on disk
            lastFile = disk.Where(x => x.Any(y => y > 0)).Last();
            lastFileBlock = Array.FindLastIndex(lastFile, x => !x.Equals(-1));

            var firstEmptySpaceIndex = disk.IndexOf(firstEmptySpace);
            var lastFileIndex = disk.IndexOf(lastFile);

            if (firstEmptySpaceIndex == lastFileIndex && firstEmptySpaceBlock > lastFileBlock) break;
            if (firstEmptySpaceIndex > lastFileIndex) break;
        }
    }

    public void PackEntireFiles()
    {
        var uniqueFileIds = disk.Where(x => !x.Any(y => y < 0) && x.Any()).Select(x => x[0]).OrderDescending();

        foreach (var uniqueFileId in uniqueFileIds)
        {
            var fileIndex = disk.FindIndex(x => x.Contains(uniqueFileId));
            var fileLength = disk[fileIndex].Length;
            var emptySpaceIndex = disk.FindIndex(x => x.Count(y => y == -1) >= fileLength);
            if (emptySpaceIndex < 0) continue;
            if (emptySpaceIndex > fileIndex) continue;
            var emptySpaceBlockStart = Array.FindIndex(disk[emptySpaceIndex], x => x == -1);
            for (var i = 0; i < fileLength; i++)
            {
                disk[emptySpaceIndex][emptySpaceBlockStart + i] = disk[fileIndex][i];
                disk[fileIndex][i] = -1;
            }
        }
    }

    public long FilesystemChecksum()
    {
        long filesystemChecksum = 0;
        int position = 0;
        foreach (var space in disk)
        {
            for (int i = 0; i < space.Length; i++)
            {
                if (space[i] >= 0)
                {
                    filesystemChecksum += space[i] * position;
                }
                position++;
            }
        }
        return filesystemChecksum;
    }

    public List<int[]> Disk => disk;
}