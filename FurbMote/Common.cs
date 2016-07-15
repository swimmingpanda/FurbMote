using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SharpCompress.Reader;
using Windows.UI;

namespace FurbMote {
  public class Common {
    /// <summary>
    /// Creates a MediaElement and plays a media file to it.
    /// </summary>
    /// <param name="source">String with the path to the file.</param>
    /// <param name="element">Element (Grid) where the MediaElement will be created.</param>
    public static void PlayMedia(string source, Grid element) {
      MediaElement mediaElement = new MediaElement();
      element.Children.Add(mediaElement); // Add MediaElement as a child to the Layout

      mediaElement.Stop(); // Stop MediaElement in case it was already playing media
      mediaElement.Source = new Uri(source);
      mediaElement.Play();
    }

    /// <summary>
    /// Extracts an archive to a folder.
    /// </summary>
    /// <param name="archive">The archive file.</param>
    /// <param name="folder">The folder to extract the contents of the archive.</param>
    public static async Task ExtractArchive(StorageFile archive, StorageFolder folder) {
      using (Stream stream = await archive.OpenStreamForReadAsync()) {
        var reader = ReaderFactory.Open(stream);
        while (reader.MoveToNextEntry()) {
          if (!reader.Entry.IsDirectory) {
            StorageFile localFile = await folder.CreateFileAsync(reader.Entry.Key, CreationCollisionOption.ReplaceExisting);
            Stream fileStream = await localFile.OpenStreamForWriteAsync();
            reader.WriteEntryTo(fileStream);
            fileStream.Dispose();
          }
        }
        reader.Dispose();
        stream.Dispose();
      }
    }

    /// <summary>
    /// Extracts a single archive to a folder. Returns the extracted file's name.
    /// </summary>
    /// <param name="archive">The archive file.</param>
    /// <param name="folder">The folder to extract the content of the archive.</param>
    public static async Task<string> ExtractSingleArchive(StorageFile archive, StorageFolder folder) {
      string name = null;
      using (Stream stream = await archive.OpenStreamForReadAsync()) {
        var reader = ReaderFactory.Open(stream);
        while (reader.MoveToNextEntry()) {
          if (!reader.Entry.IsDirectory) {
            name = reader.Entry.Key;
            StorageFile localFile = await folder.CreateFileAsync(reader.Entry.Key, CreationCollisionOption.ReplaceExisting);
            Stream fileStream = await localFile.OpenStreamForWriteAsync();
            reader.WriteEntryTo(fileStream);
            fileStream.Dispose();
          }
        }
        reader.Dispose();
        stream.Dispose();
      }
      return name;
    }

    /// <summary>
    /// Extracts the contents of an archive within an archive to a folder.
    /// </summary>
    /// <param name="archive">The archive file.</param>
    /// <param name="folder">The folder to extract the content of the archive.</param>
    /// <param name="progress">The current progress of the operation.</param>
    /// <param name="deleteSecondArchive">Optionally delete the second archive.</param>
    public static async Task ExtractDoubleArchive(StorageFile archive, StorageFolder folder, IProgress<float> progress, bool deleteSecondArchive = false) {
      progress.Report(0f);
      string fileName = await ExtractSingleArchive(archive, folder);
      progress.Report(0.25f);
      StorageFile file = await folder.GetFileAsync(fileName);
      progress.Report(0.5f);
      await ExtractArchive(file, folder);
      progress.Report(0.75f);
      if (deleteSecondArchive) await file.DeleteAsync();
      progress.Report(1.0f);
    }

    public static Color ShiftColor(Color color, byte value, bool isLight) {
      byte[] bs = { color.R, color.G, color.B };

      for (int i = 0; i < bs.Length; i++)
        bs[i] = OperateByteValue(bs[i], value, isLight);

      color = Color.FromArgb(255, bs[0], bs[1], bs[2]);
      return color;
    }

    public static byte OperateByteValue(byte b1, byte b2, bool isAddition) {
      byte bt = b1;

      if ((int.Parse(b1.ToString()) + int.Parse(b2.ToString())) > byte.MaxValue && isAddition)
        return bt = byte.MaxValue;
      else if ((int.Parse(b1.ToString()) - int.Parse(b2.ToString())) < byte.MinValue && !isAddition)
        return bt = byte.MinValue;

      if (isAddition)
        bt = (byte)(b1 + b2);
      else
        bt = (byte)(b1 - b2);

      return bt;
    }
  }
}
