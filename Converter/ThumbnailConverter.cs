using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DotnetCoreWpfExample.Converter
{
    class ThumbnailConverter : IValueConverter
    {
        private readonly Dictionary<string, BitmapImage> cache = new Dictionary<string, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = (string)value;
            if (string.IsNullOrEmpty(url)) return null;

            if (!cache.TryGetValue(url, out BitmapImage image))
            {
                image = new BitmapImage(new Uri(url));
                cache.Add(url, image);
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
