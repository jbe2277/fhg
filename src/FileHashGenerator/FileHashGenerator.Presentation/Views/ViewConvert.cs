using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Globalization;
using Waf.FileHashGenerator.Presentation.Properties;

namespace Waf.FileHashGenerator.Presentation.Views;

public static class ViewConvert
{
    public static string Format(string format, object? value) => string.Format(CultureInfo.CurrentCulture, format, value);

    public static Visibility AnyToVisibility(int count) => count > 0 ? Visibility.Visible : Visibility.Collapsed;

    public static Visibility IsEmptyToVisibility(int count) => count == 0 ? Visibility.Visible : Visibility.Collapsed;

    public static string? GetFileName(string? path) => Path.GetFileName(path);

    public static string? GetDirectoryName(string? path) => Path.GetDirectoryName(path);

    public static double NullIsOpacityZero(object? obj) => obj is null ? 0 : 1;

    public static Visibility NullIsVisible(object? obj) => obj is null ? Visibility.Visible : Visibility.Collapsed;

    public static InfoBarSeverity BoolToSeverity(bool? value) => value switch
    {
        true => InfoBarSeverity.Success,
        false => InfoBarSeverity.Error,
        _ => InfoBarSeverity.Informational
    };

    public static string IsHashValidToText(bool? isHashValid) => isHashValid switch
    {
        true => Resources.HashValid,
        false => Resources.HashNotValid,
        _ => Resources.HashNotCompared
    };
}
