using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using Waf.FileHashGenerator.Presentation.Properties;

namespace Waf.FileHashGenerator.Presentation.Converters
{
    public class IsHashValidToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isHashValid = (bool?)value;
            if (isHashValid == null)
            {
                return Resources.HashNotCompared;
            }
            else if (isHashValid == true)
            {
                return Resources.HashValid;
            }
            else
            {
                return Resources.HashNotValid;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
