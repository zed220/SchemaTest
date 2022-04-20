using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using JetBrains.Annotations;

namespace SchemaTester.Converters {
    [UsedImplicitly]
    public sealed class ColorToBrushConverterExtension : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Color color)
                return DependencyProperty.UnsetValue;

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
