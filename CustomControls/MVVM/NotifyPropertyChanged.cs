using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool HasPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyNm = null)
        {
            if ((!EqualityComparer<T>.Default.Equals(field, value)))
            {
                field = value;
                OnPropertyChanged(propertyNm);

                return true;
            }
            return false;
        }

        protected void SetPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyNm = null)
        {
            if ((!EqualityComparer<T>.Default.Equals(field, value)))
            {
                field = value;
                OnPropertyChanged(propertyNm);
            }
        }

        protected virtual void OnPropertyChanged(string property)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
