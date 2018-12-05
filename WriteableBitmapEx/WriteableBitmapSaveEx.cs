using System.IO;
using System.Windows.Media.Imaging;
using Dialogs.Windows;

namespace WriteableBitmapEx
{
    /// <summary>
    /// Exposes static methods for save an instance of the WriteableBitmap class to the file.
    /// Support extentions: jpg, jpeg, png, bmp, tiff, gif.
    /// </summary>
    public static class WriteableBitmapSaveEx
    {
        /// <summary>
        /// Save the WriteableBitmap class instance to the file as an image.
        /// Support extentions: jpg, jpeg, png, bmp, tiff, gif.
        /// </summary>
        /// <param name="source">The WriteableBitmap instance.</param>
        /// <param name="fileName">The file name to save.</param>
        public static void SaveToFile(this WriteableBitmap source, string fileName)
        {
            var index = fileName.LastIndexOf('.');
            var count = fileName.Length - index;
            string fileExtention = fileName.Substring(index, count).ToLower();

            BitmapEncoder BitmapEncoderGuid;
            switch (fileExtention.ToLower())
            {
                case ".jpg":
                    BitmapEncoderGuid = new JpegBitmapEncoder();
                    break;
                case ".jpeg":
                    BitmapEncoderGuid = new JpegBitmapEncoder();
                    break;
                case ".png":
                    BitmapEncoderGuid = new PngBitmapEncoder();
                    break;
                case ".bmp":
                    BitmapEncoderGuid = new BmpBitmapEncoder();
                    break;
                case ".tiff":
                    BitmapEncoderGuid = new TiffBitmapEncoder();
                    break;
                case ".gif":
                    BitmapEncoderGuid = new GifBitmapEncoder();
                    break;
                default:
                    return;
            }

            if (BitmapEncoderGuid is JpegBitmapEncoder bitmapEncoder)
            {
                QualityDialogResult qualityDialog = new QualityDialogResult();
                if (qualityDialog.ShowDialog() == true)
                {
                    bitmapEncoder.QualityLevel = (int)(qualityDialog.Quality * 10);
                }
            }

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                BitmapEncoderGuid.Frames.Add(BitmapFrame.Create(source));
                BitmapEncoderGuid.Save(stream);
            }
        }
    }
}
