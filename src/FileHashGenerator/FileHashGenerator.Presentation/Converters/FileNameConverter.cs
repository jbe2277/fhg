using System;
using System.Windows.Data;
using System.Globalization;
using System.IO;

namespace Waf.FileHashGenerator.Presentation.Converters
{
    public class FileNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            var path = value as string;
            return !string.IsNullOrEmpty(path) ? Path.GetFileName(path) : null;
        }

        public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotSupportedException();
    }
}
