﻿using System;
using System.IO;
using System.Linq;
using SharpCompress.Common.SevenZip;
using SharpCompress.Compressor.Filters;
using SharpCompress.Compressor.LZMA.Utilites;

namespace SharpCompress.Compressor.LZMA {

  internal static class DecoderRegistry {
    private const uint k_Copy = 0x0;
    private const uint k_Delta = 3;
    private const uint k_LZMA2 = 0x21;
    private const uint k_LZMA = 0x030101;
    private const uint k_PPMD = 0x030401;
    private const uint k_BCJ = 0x03030103;
    private const uint k_BCJ2 = 0x0303011B;
    private const uint k_Deflate = 0x040108;
    private const uint k_BZip2 = 0x040202;

    internal static Stream CreateDecoderStream(CMethodId id, Stream[] inStreams, byte[] info, IPasswordProvider pass,
                                               long limit) {
      switch (id.Id) {
        case k_Copy:
          if (info != null)
            throw new NotSupportedException();
          return inStreams.Single();

        case k_LZMA:
        case k_LZMA2:
          return new LzmaStream(info, inStreams.Single(), -1, limit);
#if !PORTABLE && !NETFX_CORE
                case CMethodId.kAESId:
                    return new AesDecoderStream(inStreams.Single(), info, pass, limit);
#endif
        case k_BCJ:
          return new BCJFilter(false, inStreams.Single());

        case k_BCJ2:
          return new Bcj2DecoderStream(inStreams, info, limit);

        default:
          throw new NotSupportedException();
      }
    }
  }
}