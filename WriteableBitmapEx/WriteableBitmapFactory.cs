using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WriteableBitmapEx
{
    /// <summary>
    /// Exposes static methods for create an instance of the WriteableBitmap class.
    /// </summary>
    public class WriteableBitmapFactory
    {
        /// <summary>
        /// Create a new instance of the WriteableBitmap class.
        /// </summary>
        /// <param name="filePath">Full path of the file.</param>
        /// <returns>The instance of the WriteableBitmap class</returns>
        public static WriteableBitmap CreateFromFile(string filePath)
        {
            BitmapImage bmpImage = BitmapImageFactory.CreateFromFile(filePath);
            return new WriteableBitmap(bmpImage);
        }

        /// <summary>
        /// Create a new instance of the WriteableBitmap class asynchronously.
        /// </summary>
        /// <param name="filePath">Full path of the file.</param>
        /// <returns>The instance of the WriteableBitmap class</returns>
        public static Task<WriteableBitmap> CreateFromFileAsync(string filePath)
        {
            WriteableBitmap wBitmap = null;
            return Task.Factory.StartNew(() => {
                BitmapImage bmpImage = BitmapImageFactory.CreateFromFile(filePath);

                wBitmap = new WriteableBitmap(bmpImage);
                wBitmap.Freeze();

                return wBitmap;
            });
        }
    }
}
