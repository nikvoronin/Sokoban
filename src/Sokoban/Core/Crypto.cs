using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sokoban.Core
{
    public class Crypto
    {
        public static byte[] Encrypt(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateEncryptor(
                (new PasswordDeriveBytes(password, null)).GetBytes(16),
                new byte[16]);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public static string Encrypt(string data, string password)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data), password));
        }

        static public byte[] Decrypt(byte[] data, string password)
        {
            BinaryReader br = new BinaryReader(InternalDecrypt(data, password));
            return br.ReadBytes((int)br.BaseStream.Length);
        }

        static public string Decrypt(string data, string password)
        {
            CryptoStream cs = InternalDecrypt(Convert.FromBase64String(data), password);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        static CryptoStream InternalDecrypt(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateDecryptor(
                (new PasswordDeriveBytes(password, null)).GetBytes(16),
                new byte[16]);

            MemoryStream ms = new MemoryStream(data);
            return new CryptoStream(ms, ct, CryptoStreamMode.Read);
        }

        public static string GetMD5Hash(string text)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(text);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }

            return s.ToString();
        }
    }
}
