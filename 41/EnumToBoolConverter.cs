using System.Globalization;
using System.Windows.Data;

namespace BMWPaint;

public class EnumToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        if (!(parameter is string parameterString))
            return false;

        if (!Enum.IsDefined(value.GetType(), value))
            return false;

        object paramValue = Enum.Parse(value.GetType(), parameterString);
        return paramValue.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolean && boolean && parameter != null)
        {
            var type = GetType(targetType);
            if (type == null)
                return null!;
            if (Enum.IsDefined(type, parameter))
            {
                return Enum.Parse(type, parameter.ToString()!);
            }
        }

        return null!;
    }

    private static Type? GetType(Type targetType)
    {
        if (targetType.IsEnum)
            return targetType;
        if (targetType.IsGenericType &&
            targetType.Namespace == "System" &&
            targetType.Name == "Nullable`1" &&
            targetType.GenericTypeArguments.Length == 1 &&
            targetType.GenericTypeArguments[0].IsEnum)
            return targetType.GenericTypeArguments[0];

        return null;
    }
}