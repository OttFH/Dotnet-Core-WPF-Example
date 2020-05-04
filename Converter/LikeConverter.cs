using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using DotnetCoreWpfExample.Models;

namespace DotnetCoreWpfExample.Converter
{
    class LikeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter)
            {
                case "like":
                    return (LikeType)value == LikeType.Like;

                case "dislike":
                    return (LikeType)value == LikeType.Dislike;
            }

            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter)
            {
                case "like":
                    return (bool)value ? LikeType.Like : LikeType.None;

                case "dislike":
                    return (bool)value ? LikeType.Dislike : LikeType.None;
            }

            throw new Exception();
        }
    }
}
