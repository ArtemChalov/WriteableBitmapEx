using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WriteableBitmapEx
{
    /// <summary>
    /// Used in some methods is affect to method logic.
    /// </summary>
    public enum DesiredSize
    {
        /// <summary>
        /// Width is used in a method logic
        /// </summary>
        Width = 0,
        /// <summary>
        /// Height is used in a method logic
        /// </summary>
        Height = 1
    }

    /// <summary>
    /// Exposes static methods for create an instance of the BitmapImage class.
    /// </summary>
    public static class BitmapImageFactory
    {
        /// <summary>
        /// Create a new instance of the BitmapImage class.
        /// </summary>
        /// <param name="filePath">Full path of the file.</param>
        /// <returns>The instance of the BitmapImage class.</returns>
        public static BitmapImage CreateFromFile(string filePath)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
            }
            catch (Exception)
            {
                MessageBox.Show("Файл имеет не верный формат\nили поврежден.", "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return bitmap;
        }

        /// <summary>
        /// Create a new instance of the BitmapImage class asynchronously.
        /// </summary>
        /// <param name="filePath">Full path of the file.</param>
        /// <returns>The instance of the BitmapImage class.</returns>
        public static Task<BitmapImage> CreateFromFileAsync(string filePath)
        {
            BitmapImage bitmap = null;
            return Task.Factory.StartNew(() => {
                try
                {
                    bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(filePath);
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                catch (Exception)
                {
                    MessageBox.Show("Файл имеет не верный формат\nили поврежден.", "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return bitmap;
            });
        }

        /// <summary>
        /// Create a new resized instance of the BitmapImage class with desired width.
        /// </summary>
        /// <param name="filePath">Full path of the file.</param>
        /// <param name="desiredSize">Desired width or height.</param>
        /// <param name="kindOfSize">Desired kind of size to be used.</param>
        /// <returns>The instance of the BitmapImage class.</returns>
        public static BitmapImage CreateThumbnailFromFile(string filePath, int desiredSize, DesiredSize kindOfSize)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                if (kindOfSize == DesiredSize.Width)
                    bitmap.DecodePixelWidth = desiredSize;
                else
                    bitmap.DecodePixelHeight = desiredSize;
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
            }
            catch (Exception)
            {
                MessageBox.Show("Файл имеет не верный формат\nили поврежден.", "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return bitmap;
        }
    }
}
