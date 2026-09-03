using System.IO.Compression;
using System.Text;
using Sokoban.Core;

namespace Sokoban.Tests;

public class LoaderTests
{
    private static Stream TextStream(string content) =>
        new MemoryStream(Encoding.UTF8.GetBytes(content));

    [Fact]
    public void LoadPack_PlainText_ReturnsSinglePackWithGivenNameAndAllLevels()
    {
        const string pack = "Level One\r\n#@$.#\r\n\r\nLevel Two\r\n#@$.#\r\n\r\n";

        List<LevelPack> packs = Loader.LoadPack(TextStream(pack), "userDefinedLevels.pack");

        LevelPack single = Assert.Single(packs);
        Assert.Equal("userDefinedLevels", single.Name);
        Assert.Equal(2, single.Levels.Count);
        Assert.Equal("Level One", single.Levels[0].Name);
        Assert.Equal("Level Two", single.Levels[1].Name);
    }

    [Fact]
    public void LoadPack_ZipWithMultipleEntries_ReturnsOnePackPerEntry()
    {
        using MemoryStream zipStream = new();
        using (ZipArchive zip = new(zipStream, ZipArchiveMode.Create, leaveOpen: true))
        {
            AddEntry(zip, "a.txt", "Level A1\r\n#@$.#\r\n\r\nLevel A2\r\n#@$.#\r\n");
            AddEntry(zip, "b.txt", "Level B1\r\n#@$.#\r\n");
        }
        zipStream.Seek(0, SeekOrigin.Begin);

        List<LevelPack> packs = Loader.LoadPack(zipStream, "ignored.zip");

        Assert.Equal(2, packs.Count);

        LevelPack packA = Assert.Single(packs, p => p.Name == "a");
        Assert.Equal(2, packA.Levels.Count);
        Assert.Equal("Level A1", packA.Levels[0].Name);
        Assert.Equal("Level A2", packA.Levels[1].Name);

        LevelPack packB = Assert.Single(packs, p => p.Name == "b");
        Assert.Equal(["Level B1"], packB.Levels.Select(l => l.Name));
    }

    [Fact]
    public void LoadPack_StripsFileExtensionFromPackName()
    {
        List<LevelPack> packs = Loader.LoadPack(TextStream("Level One\r\n#@$.#\r\n\r\n"), "Rabbit.pack");

        Assert.Equal("Rabbit", Assert.Single(packs).Name);
    }

    [Fact]
    public void LoadPack_LastLevelWithoutTrailingBlankLine_IsStillParsed()
    {
        const string pack = "Level One\r\n#@$.#\r\n";

        List<LevelPack> packs = Loader.LoadPack(TextStream(pack), "pack.pack");

        Level level = Assert.Single(Assert.Single(packs).Levels);
        Assert.Equal("Level One", level.Name);
    }

    private static void AddEntry(ZipArchive zip, string entryName, string content)
    {
        ZipArchiveEntry entry = zip.CreateEntry(entryName);
        using Stream entryStream = entry.Open();
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        entryStream.Write(bytes, 0, bytes.Length);
    }
}
