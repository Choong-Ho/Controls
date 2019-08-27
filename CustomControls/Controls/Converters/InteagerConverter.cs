using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Controls.Converters
{
    /// <summary>
    /// 바인딩 모드가 TwoWay이며 Text속성의 타입과 바인딩된 속성의 타입이 다르므로(string/int) 
    /// 컨버터를 이용해 알맞은 타입으로 변환하여 리턴한다.(이렇게 안해도 작동은 하지만, 바인딩 에러를 일으키므로..)
    /// </summary>
    internal sealed class InteagerConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => string.Format("{0}", value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => System.Convert.ToInt32(value);

        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}
