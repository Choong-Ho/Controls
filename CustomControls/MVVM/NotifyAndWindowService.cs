using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM
{
    public abstract class NotifyAndWindowService : NotifyPropertyChanged, ICreateWindowService
    {
        protected IDialogWindow DialogWindowService => CreateService<IDialogWindow>();
        protected IMessageBoxWindow MsgBoxWindowService => CreateService<IMessageBoxWindow>();
        public virtual T CreateService<T>() where T : class
        {
            var type = typeof(T);
            var targetType = Application.Current.Resources.Keys.OfType<Type>().FirstOrDefault(t => type.IsAssignableFrom(t));

            /*var targetStyle = Application.Current.Resources.Values.OfType<Style>().FirstOrDefault(s => type.IsAssignableFrom(s.TargetType)); 
             * => App.xaml에 스타일이 재정의 되어 있으면.. 
             * 현재는 스타일을 재정의 하지 않으므로 필요 없음!!!!!!!!!*/

            if (targetType != null)
                return (T)Activator.CreateInstance(targetType);
            else
                return default(T);
        }
    }
}
