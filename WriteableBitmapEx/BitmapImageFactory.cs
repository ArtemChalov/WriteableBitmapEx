using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WriteableBitmapEx
{
    /// <summary>
    /// Exposes static methods for create an instance of the BitmapImage class.
    /// </summary>
    public static class BitmapImageFactory
    {
        /// <summary>
        /// Create a new instance of the BitmapImage class.
        /// </summary>
        /// <param name="filePath">Full path of the file.</param>
        /// <returns>The instance of the BitmapImage class</returns>
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
        /// <returns>The instance of the BitmapImage class</returns>
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
    }
}
