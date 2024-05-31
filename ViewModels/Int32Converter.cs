using Avalonia.Data.Converters;
using System.Globalization;
using System;

namespace AndreyKursovaya.ViewModels;

public class Int32Converter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return 0;
        }
        return System.Convert.ToInt32(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || value.ToString() == "0")
        {
            return 0;
        }

        if (int.TryParse(value.ToString(), out int result))
        {
            return result;
        }
        else
        {
            return 0;
        }
    }
}
