using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace Waf.FileHashGenerator.Presentation.Converters
{
    public class IsHashValidToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isHashValid = (bool?)value;
            if (isHashValid == null)
            {
                return Application.Current.FindResource("InformationImageSource");
            }
            else if (isHashValid == true)
            {
                return Application.Current.FindResource("CompleteImageSource");
            }
            else
            {
                return Application.Current.FindResource("ErrorImageSource");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
