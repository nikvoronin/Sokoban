using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Sokoban.Core;

public static class Loader
{
    private static bool IsFilePacked(Stream stream)
    {
        long pos = stream.Position;
        stream.Seek(0, SeekOrigin.Begin);
        byte[] buffer = new byte[2];
        int readed = stream.Read(buffer, 0, 2);

        stream.Seek(pos, SeekOrigin.Begin);

        // PK - 2 bytes length .zip signature
        return 
            readed == 2 
            && buffer[0] == 'P' 
            && buffer[1] == 'K';
    }

    public static List<LevelPack> LoadPack(Stream stream, string name)
    {
        return IsFilePacked(stream)
            ? LoadFromZip(stream)
            : [new LevelPack(Path.GetFileNameWithoutExtension(name), LoadFromText(stream))];
    }

    private static List<LevelPack> LoadFromZip(Stream packedStream)
    {
        List<LevelPack> packs = [];
        using ZipArchive zip = new(packedStream, ZipArchiveMode.Read);
        foreach (ZipArchiveEntry entry in zip.Entries)
        {
            using Stream entryStream = entry.Open();
            packs.Add(new LevelPack(Path.GetFileNameWithoutExtension(entry.Name), LoadFromText(entryStream)));
        }
        return packs;
    }

    private static List<Level> LoadFromText(Stream stream)
    {
        List<Level> levels = [];

        TextReader reader = new StreamReader(stream);

        string? lineBuffer;
        int blockNo = 0;
        string name = "";
        StringBuilder builder = new();
        while ((lineBuffer = reader.ReadLine()) != null)
        {
            if (blockNo == 1)
            {
                if (lineBuffer.Trim().Length < 1)
                {
                    string rawMap = builder.ToString();

                    Level newLevel = new(name, rawMap);
                    levels.Add(newLevel);

                    name = "";
                    builder.Clear();

                    blockNo = 0;
                    continue;
                }
            }

            switch (blockNo)
            {
                case 0: // level name
                    name = lineBuffer.Trim();
                    blockNo = 1;
                    break;

                case 1: // level map
                    builder.AppendLine(lineBuffer);
                    break;
            }
        }

        if (blockNo == 1)
            levels.Add(new Level(name, builder.ToString()));

        reader.Close();

        return levels;
    }

    public static Stream OpenFile(string name)
    {
        return File.OpenRead(name);
    }

    public static Stream OpenEmbeddedResource(string name)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        return asm.GetManifestResourceStream(name)
            ?? throw new FileNotFoundException($"Embedded resource not found: {name}");
    }
}
