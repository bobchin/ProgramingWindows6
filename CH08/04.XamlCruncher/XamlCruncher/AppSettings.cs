using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using XamlCruncher.Properties;

namespace XamlCruncher
{
    public class AppSettings : INotifyPropertyChanged
    {
        // アプリケーション設定の初期値
        EditOrientation editOrientation = EditOrientation.Left;
        Orientation orientation = Orientation.Horizontal;
        bool swapEditAndDisplay = false;
        bool autoParsing = true;
        bool showRuler = false;
        bool showGridLines = false;
        double fontSize = 18;
        int tabSpaces = 4;

        public AppSettings()
        {
            //ApplicationDataContainer appData = ApplicationData.Current.LocalSettings;
            Settings settings = Settings.Default;

            this.EditOrientation = (EditOrientation)settings.EditOrientation;
            this.AutoParsing = settings.AutoParsing;
            this.ShowRuler = settings.ShowRuler;
            this.ShowGridLines = settings.ShowGridLines;
            this.FontSize = settings.FontSize;
            this.TabSpaces = settings.TabSpaces;
        }

        public EditOrientation EditOrientation
        {
            set
            {
                if (SetProperty<EditOrientation>(ref editOrientation, value))
                {
                    switch (editOrientation)
                    {
                        case EditOrientation.Left:
                            this.Orientation = Orientation.Horizontal;
                            this.SwapEditAndDisplay = false;
                            break;
                        case EditOrientation.Top:
                            this.Orientation = Orientation.Vertical;
                            this.SwapEditAndDisplay = false;
                            break;
                        case EditOrientation.Right:
                            this.Orientation = Orientation.Horizontal;
                            this.SwapEditAndDisplay = true;
                            break;
                        case EditOrientation.Bottom:
                            this.Orientation = Orientation.Vertical;
                            this.SwapEditAndDisplay = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            get { return editOrientation; }
        }

        public Orientation Orientation
        {
            protected set
            {
                SetProperty<Orientation>(ref orientation, value);
            }
            get { return orientation; }
        }

        public bool SwapEditAndDisplay
        {
            protected set { SetProperty<bool>(ref swapEditAndDisplay, value); }
            get { return swapEditAndDisplay; }
        }

        public bool AutoParsing
        {
            set { SetProperty<bool>(ref autoParsing, value); }
            get { return autoParsing; }
        }

        public bool ShowRuler
        {
            set { SetProperty<bool>(ref showRuler, value); }
            get { return showRuler; }
        }

        public bool ShowGridLines
        {
            set { SetProperty<bool>(ref showGridLines, value); }
            get { return showGridLines; }
        }

        public double FontSize
        {
            set { SetProperty<double>(ref fontSize, value); }
            get { return fontSize; }
        }

        public int TabSpaces
        {
            set { SetProperty<int>(ref tabSpaces, value); }
            get { return tabSpaces; }
        }

        public void Save()
        {
            Settings settings = Settings.Default;
            settings.EditOrientation = (int)this.EditOrientation;
            settings.AutoParsing = this.AutoParsing;
            settings.ShowRuler = this.ShowRuler;
            settings.ShowGridLines = this.ShowGridLines;
            settings.FontSize = this.FontSize;
            settings.TabSpaces = this.TabSpaces;

            settings.Save();
            settings.Reload();
        }

        #region INotifyPropertyChanged の実装（＝ビューモデルの実装）
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnProperyChanged(propertyName);
            return true;
        }

        protected void OnProperyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
