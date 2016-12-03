using System;
using System.IO;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.IO;
using SharpCompress.Reader.Zip;

namespace SharpCompress.Reader {

  public static class ReaderFactory {

    // //
    /// <summary>
    /// Opens a Reader for Non-seeking usage
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IReader Open(Stream stream, Options options = Options.KeepStreamsOpen) {
      stream.CheckNotNull("stream");

      RewindableStream rewindableStream = new RewindableStream(stream);
      rewindableStream.StartRecording();
      if (ZipArchive.IsZipFile(rewindableStream, null)) {
        rewindableStream.Rewind(true);
        return ZipReader.Open(rewindableStream, null, options);
      }

      throw new InvalidOperationException("Cannot determine compressed stream type.  Supported Reader Formats: Zip, GZip, BZip2, Tar, Rar");
    }
  }
}