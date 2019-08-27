using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using MVVM;
using static Extension.ReactiveUI.ReactiveCommandEx;
using NLog;
using System.Collections.ObjectModel;

namespace ExampleApp.ViewModel
{
    public sealed class TestDialogViewModel : DialogBaseViewModel, IDisposable
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly List<IDisposable> _disposers = new List<IDisposable>();

        private ObservableCollection<string> _tests;
        public ObservableCollection<string> Tests
        {
            get => _tests ?? (_tests = new ObservableCollection<string>(Enumerable.Range(1, 100).Select(n => $"테스트 아이템.{n}")));
            set => SetPropertyChanged(ref _tests, value);
        }

        private ICommand _confirmCommand;
        public override ICommand ConfirmCommand
            => _confirmCommand ?? (_confirmCommand = CreateCommandWithThrownEx<CancelEventArgs>(Confirm, _logger.Error, disposers: _disposers));

        public TestDialogViewModel() 
            : base(typeof(View.TestDialogView))
        {

        }

        private void Confirm(CancelEventArgs args)
        {

        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _disposers.ForEach(_ => _.Dispose());
                    _disposers.Clear();

                    Tests?.Clear();
                    Tests = null;
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~TestDialogViewModel()
        // {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
