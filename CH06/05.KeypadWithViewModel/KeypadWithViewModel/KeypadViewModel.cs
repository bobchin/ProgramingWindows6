using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KeypadWithViewModel
{
    public class KeypadViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string inputString = "";
        private string displayText = "";
        char[] specialChars = { '*', '#' };

        public KeypadViewModel()
        {
            this.AddCharactorCommand = new DelegateCommand(ExecuteAddCharactor);
            this.DeleteCharactorCommand = 
                new DelegateCommand(ExecuteDeleteCharactor, CanExecuteDeleteCharactor);
        }

        #region プロパティ
        public string InputString
        {
            protected set
            {
                bool previousCanExecuteDeleteChar = this.CanExecuteDeleteCharactor(null);
                if (this.SetProperty<string>(ref inputString, value))
                {
                    this.DisplayText = FormatText(inputString);
                    if (previousCanExecuteDeleteChar != this.CanExecuteDeleteCharactor(null))
                    {
                        this.DeleteCharactorCommand.RaiseCanExecuteChanged();
                    }
                }
            }

            get { return inputString; }
        }

        public string DisplayText
        {
            set { this.SetProperty<string>(ref displayText, value); }
            get { return displayText; }
        }
        #endregion


        private string FormatText(string str)
        {
            bool hasNonNumbers = (str.IndexOfAny(specialChars) != -1);
            string formatted = str;

            if (hasNonNumbers || str.Length < 4 || str.Length > 10)
            {
                // なにもしない
            }
            // 4文字以上8文字未満
            else if (str.Length < 8)
            {
                formatted = String.Format("{0}-{1}", str.Substring(0, 3), str.Substring(3));
            }
            // 8文字以上10文字以下
            else
            {
                formatted = String.Format("({0}) {1}-{2}",
                    str.Substring(0, 3), str.Substring(3, 3), str.Substring(6));
            }

            return formatted;
        }


        #region ICommandの実装
        public IDelegateCommand AddCharactorCommand { protected set; get; }
        public IDelegateCommand DeleteCharactorCommand { protected set; get; }

        private void ExecuteAddCharactor(object param)
        {
            this.InputString += param as string;
        }
        private void ExecuteDeleteCharactor(object param)
        {
            this.InputString = this.InputString.Substring(0, this.InputString.Length - 1);
        }
        private bool CanExecuteDeleteCharactor(object param)
        {
            return this.InputString.Length > 0;
        }
        #endregion


        #region ビューモデル化するためのINotifyPropertyChanged用の汎用メソッド
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
