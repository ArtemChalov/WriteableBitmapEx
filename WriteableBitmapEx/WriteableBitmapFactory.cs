﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WriteableBitmapEx
{
    public class WriteableBitmapFactory
    {
        public static WriteableBitmap CreateFromFile(string filePath)
        {
            BitmapImage bmpImage = new BitmapImage();
            WriteableBitmap wBitmap = null;
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    bmpImage.BeginInit();
                    bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                    bmpImage.StreamSource = fs;
                    bmpImage.EndInit();
                }

                wBitmap = new WriteableBitmap(bmpImage);
            }
            catch
            {
                MessageBox.Show("Файл имеет не верный формат\nили поврежден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                bmpImage.StreamSource = null;
                bmpImage = null;
            }
            return wBitmap;
        }
    }
}
