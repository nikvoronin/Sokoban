using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Sokoban
{
    public static class Loader
    {
        private static bool IsFilePacked(string name)
        {
            Stream fs = File.OpenRead(name);
            bool isPacked = IsFilePacked(fs);
            fs.Close();

            return isPacked;
        }

        private static bool IsFilePacked(Stream stream)
        {
            long pos = stream.Position;
            stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[2];
            int readed = stream.Read(buffer, 0, 2);

            stream.Seek(pos, SeekOrigin.Begin);

            // PK - 2 bytes length .zip signature
            return readed == 2 && buffer[0] == 'P' && buffer[1] == 'K';
        }

        public static List<Level> LoadPack(Stream stream)
        {
            List<Level> levels = new List<Level>();

            TextReader reader = new StreamReader(stream);

            string lineBuffer;
            int blockNo = 0;
            string name = "";
            StringBuilder builder = new StringBuilder();
            while ((lineBuffer = reader.ReadLine()) != null)
            {
                if (blockNo == 1)
                {
                    if (lineBuffer.Trim().Length < 1)
                    {
                        string rawMap = builder.ToString();

                        Level newLevel = new Level(name, rawMap);
                        levels.Add(newLevel);

                        name = "";
                        builder.Clear();

                        blockNo = 0;
                        continue;
                    } // if (lineBuffer.Trim().Length < 1)
                } // if (blockNo == 1)

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
            } // while ((lineBuffer = reader.ReadLine()) != null)

            reader.Close();

            return levels;
        } // LoadPack(Stream stream)

        private static Stream Unpack(Stream packedStream)
        {
            Stream memStream = new MemoryStream();
            ZipStorer zip = ZipStorer.Open(packedStream, FileAccess.Read);
            List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
            zip.ExtractFile(dir[0], memStream);
            memStream.Seek(0, SeekOrigin.Begin);

            return memStream;
        }

        public static Stream OpenFile(string name)
        {
            Stream stream = File.OpenRead(name);

            return
                IsFilePacked(name) ?
                    Unpack(stream) :
                    stream;
        }

        public static Stream OpenEmbeddedResource(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream embStream = asm.GetManifestResourceStream(name);

            return
                IsFilePacked(embStream) ?
                    Unpack(embStream) :
                    embStream;
        }
    }
}
