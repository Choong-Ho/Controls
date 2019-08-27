using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Controls
{
    internal class MessageBoxResultButton : Button
    {
        public static readonly string YES;
        public static readonly string NO;
        public static readonly string OK;
        public static readonly string CANCEL;
        
        public MessageBoxResult MessageBoxResult { get; }

        static MessageBoxResultButton()
        {
            if (CultureInfo.CurrentCulture.IetfLanguageTag == "ko-KR")
            {
                YES = "예(_Y)";
                NO = "아니오(_N)";
                OK = "확인(_O)";
                CANCEL = "취소(_C)";
            }
            else
            {
                YES = "Yes(_Y)";
                NO = "No(_N)";
                OK = "Ok(_O)";
                CANCEL = "Cancel(_C)";
            }
        }

        internal MessageBoxResultButton(string content)
        {
            Content = new AccessText { Text = content };
        }

        internal MessageBoxResultButton(MessageBoxResult messageBoxResult)
        {
            MessageBoxResult = messageBoxResult;
            SetContent(messageBoxResult);
        }

        private void SetContent(MessageBoxResult messageBoxResult)
        {
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    Content = new AccessText { Text = YES };
                    break;
                case MessageBoxResult.No:
                    Content = new AccessText { Text = NO };
                    break;
                case MessageBoxResult.OK:
                    Content = new AccessText { Text = OK };
                    break;
                case MessageBoxResult.Cancel:
                    Content = new AccessText { Text = CANCEL };
                    break;
            }
        }

        internal MessageBoxResultButton() { }
    }
}
