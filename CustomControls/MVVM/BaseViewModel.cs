using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;

namespace MVVM
{
    public abstract class BaseViewModel : NotifyAndWindowService, ICurrent, IDisposable
    {
        public string CurrentGUID { get; }

        public BaseViewModel()
        {
            CurrentGUID = Guid.NewGuid().ToString();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
