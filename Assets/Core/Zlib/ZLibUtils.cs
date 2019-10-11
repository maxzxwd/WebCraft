using System.IO;
using System;


namespace Ionic.Zlib
{
    public static class ZlibUtils {
        public static byte[] Decompress(MemoryStream data, int size) {
            byte[] msDecompressed = new byte[size];
            var zOut = new ZlibStream(data, CompressionMode.Decompress);
            zOut.Read(msDecompressed, 0, msDecompressed.Length);
            zOut.Close();
            return msDecompressed;
        }

        public static MemoryStream Compress(MemoryStream data, CompressionLevel level) {
            var msCompressed = new MemoryStream();
            var zOut = new ZlibStream(msCompressed, CompressionMode.Compress, level);
            zOut.Write(data.GetBuffer(), 0, (int) data.Length);
            zOut.Close();
            return msCompressed;
        }
    }
}