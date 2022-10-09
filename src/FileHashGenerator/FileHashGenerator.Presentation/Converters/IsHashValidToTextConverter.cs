using System.Windows.Data;
using System.Globalization;
using Waf.FileHashGenerator.Presentation.Properties;

namespace Waf.FileHashGenerator.Presentation.Converters;

public class IsHashValidToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        bool? isHashValid = (bool?)value;
        return isHashValid switch
        {
            null => Resources.HashNotCompared,
            true => Resources.HashValid,
            _ => Resources.HashNotValid
        };
    }

    public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) => throw new NotSupportedException();
}
