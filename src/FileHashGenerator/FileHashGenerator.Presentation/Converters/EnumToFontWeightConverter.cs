using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace Waf.FileHashGenerator.Presentation.Converters
{
    public class EnumToFontWeightConverter : IValueConverter
    {
        public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            return Equals(value?.ToString(), parameter as string) ? FontWeights.SemiBold : DependencyProperty.UnsetValue;
        }

        public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotSupportedException();
    }
}
