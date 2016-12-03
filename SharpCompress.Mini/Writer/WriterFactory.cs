using System;
using System.IO;
using SharpCompress.Common;
using SharpCompress.Writer.Zip;

namespace SharpCompress.Writer {

  public static class WriterFactory {

    public static IWriter Open(Stream stream, ArchiveType archiveType, CompressionType compressionType) {
      return Open(stream, archiveType, new CompressionInfo {
        Type = compressionType
      });
    }

    public static IWriter Open(Stream stream, ArchiveType archiveType, CompressionInfo compressionInfo) {
      switch (archiveType) {
        case ArchiveType.Zip: {
            return new ZipWriter(stream, compressionInfo, null);
          }
        default: {
            throw new NotSupportedException("Archive Type does not have a Writer: " + archiveType);
          }
      }
    }
  }
}