using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Windows.Input;
using MVVM;
using ReactiveUI;
using static Extension.ReactiveUI.ReactiveCommandEx;

namespace ExampleApp.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly List<IDisposable> _disposers = new List<IDisposable>();

        private ICommand _dialogShowCommand;
        public ICommand DialogShowCommand
            => _dialogShowCommand ?? (_dialogShowCommand = CreateCommandWithThrownEx(DialogShow, _logger.Error, disposers: _disposers));

        private ICommand _normalMsgBoxShowCommand;
        public ICommand NormalMsgBoxShowCommand
            => _normalMsgBoxShowCommand ?? (_normalMsgBoxShowCommand = CreateCommandWithThrownEx(NormalMsgBoxShow, _logger.Error, disposers: _disposers));

        private ICommand _warningMsgBoxShowCommand;
        public ICommand WarningMsgBoxShowCommand
            => _warningMsgBoxShowCommand ?? (_warningMsgBoxShowCommand = CreateCommandWithThrownEx(WarningMsgBoxShow, _logger.Error, disposers: _disposers));

        private ICommand _errorMsgBoxShowCommand;
        public ICommand ErrorMsgBoxShowCommand
            => _errorMsgBoxShowCommand ?? (_errorMsgBoxShowCommand = CreateCommandWithThrownEx(ErrorMsgBoxShow, _logger.Error, disposers: _disposers));

        private ICommand _questionMsgBoxShowCommand;
        public ICommand QuestionMsgBoxShowCommand
            => _questionMsgBoxShowCommand ?? (_questionMsgBoxShowCommand = CreateCommandWithThrownEx(QuestionMsgBoxShow, _logger.Error, disposers: _disposers));

        private ICommand _infoMsgBoxShowCommand;
        public ICommand InfoMsgBoxShowCommand
            => _infoMsgBoxShowCommand ?? (_infoMsgBoxShowCommand = CreateCommandWithThrownEx(InfoMsgBoxShow, _logger.Error, disposers: _disposers));

        private void NormalMsgBoxShow()
        {
            var result = MsgBoxWindowService.Show("메시지 박스 호출에 이용되는 메서드 입니다.", "캡션", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Asterisk);
        }

        private void WarningMsgBoxShow()
        {
            var result = MsgBoxWindowService.ShowWarning("경고 메시지 박스 입니다.");
        }

        private void ErrorMsgBoxShow()
        {
            var result = MsgBoxWindowService.ShowError("오류 메시지 박스 입니다.");
        }

        private void QuestionMsgBoxShow()
        {
            var result = MsgBoxWindowService.ShowQuestion("질문 메시지 박스 입니다.");
        }

        private void InfoMsgBoxShow()
        {
            var result = MsgBoxWindowService.ShowInformation("정보 메시지 박스 입니다.");
        }

        private void DialogShow()
        {
            TestDialogViewModel vm = new TestDialogViewModel();
            DialogWindowService.ShowDialog("다이얼로그", System.Windows.MessageBoxButton.OKCancel, vm);
            //DialogWindowService.ShowDialog("다이얼로그", System.Windows.MessageBoxButton.OK, vm, System.Windows.ResizeMode.CanResize);
            //Resize 가능, 단 최소 사이즈는 정해짐(View의 최초 사이즈를 최소 사이즈로 적용)
        }

        protected override void Dispose(bool disposing)
        {
            _disposers.ForEach(_ => _.Dispose());
            _disposers.Clear();

            base.Dispose(disposing);
        }
    }
}
