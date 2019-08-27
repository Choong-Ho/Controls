using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Controls
{
    internal sealed class DateConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 2 && (values[1] == null || values[1] is DateTime?) && values[0] is DateTime)
            {
                var selectedDate = (DateTime?)values[1];

                return selectedDate ?? values[0];
            }
            else
                throw new ArgumentException("Unexpected", "values");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => null;

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}
