using System.IO.Compression;
using System.Text;
using Sokoban.Core;

namespace Sokoban.Tests;

public class LoaderTests
{
    private static Stream TextStream(string content) =>
        new MemoryStream(Encoding.UTF8.GetBytes(content));

    [Fact]
    public void LoadPack_PlainText_ParsesAllLevelBlocks()
    {
        const string pack = "Level One\r\n#@$.#\r\n\r\nLevel Two\r\n#@$.#\r\n\r\n";

        List<Level> levels = Loader.LoadPack(TextStream(pack));

        Assert.Equal(2, levels.Count);
        Assert.Equal("Level One", levels[0].Name);
        Assert.Equal("Level Two", levels[1].Name);
    }

    [Fact]
    public void LoadPack_ZipWithMultipleEntries_MergesLevelsFromAllEntries()
    {
        using MemoryStream zipStream = new();
        using (ZipArchive zip = new(zipStream, ZipArchiveMode.Create, leaveOpen: true))
        {
            AddEntry(zip, "a.txt", "Level A1\r\n#@$.#\r\n\r\nLevel A2\r\n#@$.#\r\n\r\n");
            AddEntry(zip, "b.txt", "Level B1\r\n#@$.#\r\n\r\n");
        }
        zipStream.Seek(0, SeekOrigin.Begin);

        List<Level> levels = Loader.LoadPack(zipStream);

        Assert.Equal(3, levels.Count);
        Assert.Contains(levels, l => l.Name == "Level A1");
        Assert.Contains(levels, l => l.Name == "Level A2");
        Assert.Contains(levels, l => l.Name == "Level B1");
    }

    private static void AddEntry(ZipArchive zip, string entryName, string content)
    {
        ZipArchiveEntry entry = zip.CreateEntry(entryName);
        using Stream entryStream = entry.Open();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        entryStream.Write(bytes, 0, bytes.Length);
    }
}
