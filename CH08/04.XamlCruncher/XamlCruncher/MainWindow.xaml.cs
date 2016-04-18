using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;

namespace XamlCruncher
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush textBlockBrush, textBoxBrush, errorBrush;

        private AppSettings appSettings;
        //private StorageFile loadedStorageFile;
        string loadedStorageFile;
        TabbableTextBox editBox;
        RulerContainer resultContainer;

        string fileName = "XamlCruncher.xaml";

        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            // ブラシを設定
            textBlockBrush = new SolidColorBrush(Colors.Black);
            textBoxBrush = new SolidColorBrush(Colors.Black);
            errorBrush = new SolidColorBrush(Colors.Red);

            // これらを生成されたVisual C#ファイルで設定しないのはなぜか？
            editBox = splitContainer.Child1 as TabbableTextBox;
            resultContainer = splitContainer.Child2 as RulerContainer;

            // TextBoxの固定幅フォントを設定
            //Language language = new Language(Windows.Globalization.Language.CurrentInputMethodLanguageTag);
            //LanguageFontGroup languageFontGroup = new LanguageFontGroup(language.LanguageTag);
            //LanguageFont languageFont = languageFontGroup.FixedWidthTextFont;
            //editBox.FontFamily = new FontFamily(languageFont.FontFamily);

            Loaded += OnLoaded;

            //Application.Current.Suspending += OnApplicationSuspending;
            Closing += MainWindow_Closing;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            // AppSettingsをロードし、DataContextに設定
            appSettings = new AppSettings();
            this.DataContext = appSettings;
            appSettings.AutoParsing = true;

            // 保存されているファイルがあれば、それらをロード
            //StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //StorageFile storageFile = await localFolder.CreateFileAsync("XamlCruncher.xaml",
            //                                        CreationCollisionOption.OpenIfExists);
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (FileStream stream = File.Open(System.IO.Path.Combine(documents, fileName), FileMode.OpenOrCreate))
            {
                TextReader reader = (TextReader)new StreamReader(stream);
                editBox.Text = await reader.ReadToEndAsync();
            }

            if (editBox.Text.Length == 0)
            {
                SetDefaultXamlFile();
            }

            // その他の初期化
            ParseText();
            //editBox.Focus(FocusState.Programmatic);
            editBox.Focus();
            DisplayLineAndColumn();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (appSettings != null)
            {
                appSettings.Save();
            }
        }

        private void SetDefaultXamlFile()
        {
            editBox.Text =
                "<StackPanel xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n" +
                "      xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n" +
                "      Orientation=\"Vertical\">\r\n\r\n" +
                "    <TextBlock Text=\"Hello, Windows 8!\"\r\n" +
                "               FontSize=\"48\" />\r\n\r\n" +
                "</StackPanel>";

            editBox.IsModified = false;
            loadedStorageFile = "";
            filenameText.Text = "";
        }

        private void OnEditBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            DisplayLineAndColumn();
        }

        private void DisplayLineAndColumn()
        {
            int line, col;
            editBox.GetPositionFromIndex(editBox.SelectionStart, out line, out col);
            lineColText.Text = String.Format("Line {0} Col {1}", line + 1, col + 1);

            if (editBox.SelectionLength > 0)
            {
                editBox.GetPositionFromIndex(editBox.SelectionStart + editBox.SelectionLength - 1,
                                             out line, out col);
                lineColText.Text += String.Format(" - Line {0} Col {1}", line + 1, col + 1);
            }
        }

        private void OnRefreshAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            ParseText();
            //this.BottomAppBar.IsOpen = false;
        }

        private void OnEditBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (appSettings.AutoParsing)
            {
                ParseText();
            }
        }

        private void ParseText()
        {
            object result = null;
            try
            {
                result = XamlReader.Parse(editBox.Text);
            }
            catch (Exception exc)
            {
                SetErrorText(exc.Message);
                return;
            }

            if (result == null)
            {
                SetErrorText("Null result");
            }
            else if (!(result is UIElement))
            {
                SetErrorText("Result is " + result.GetType().Name);
            }
            else
            {
                resultContainer.Child = result as UIElement;
                SetOkText();
                return;
            }
        }

        private void SetErrorText(string text)
        {
            SetStatusText(text, errorBrush, errorBrush);
        }

        private void SetOkText()
        {
            SetStatusText("OK", textBlockBrush, textBoxBrush);
        }

        private void SetStatusText(string text, Brush statusBrush, Brush editBrush)
        {
            statusText.Text = text;
            statusText.Foreground = statusBrush;
            editBox.Foreground = editBrush;
        }

        #region 保存処理

        /// <summary>
        /// 上書き保存
        /// </summary>
        private void OnSaveAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(loadedStorageFile))
            {
                SaveXamlToFile(loadedStorageFile);
            }
            else
            {
                string storageFile = GetFileFromSavePicker();

                if (!string.IsNullOrEmpty(storageFile))
                {
                    SaveXamlToFile(storageFile);
                }
            }
        }

        /// <summary>
        /// ファイル名を指定して保存
        /// </summary>
        private void OnSaveAsAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            string storageFile = GetFileFromSavePicker();
            if (string.IsNullOrEmpty(storageFile))
            {
                return;
            }
            SaveXamlToFile(storageFile);
        }

        /// <summary>
        /// 保存ダイアログを表示して保存ファイル名を取得する
        /// </summary>
        string GetFileFromSavePicker()
        {
            //FileSavePicker picker = new FileSavePicker();
            Microsoft.Win32.SaveFileDialog picker = new Microsoft.Win32.SaveFileDialog();
            picker.DefaultExt = ".xaml";
            picker.Filter = "XAML(*.xaml)|*.xaml";
            picker.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string name = fileName;
            if (!string.IsNullOrEmpty(loadedStorageFile))
            {
                name = System.IO.Path.GetFileName(loadedStorageFile);
            }
            picker.FileName = name;

            var pickerResult = picker.ShowDialog();
            if (pickerResult == true)
            {
                return picker.FileName;
            }
            return "";

        }

        /// <summary>
        /// ファイルに保存する
        /// </summary>
        void SaveXamlToFile(string storageFile)
        {
            loadedStorageFile = storageFile;
            string exception = null;

            try
            {
                File.WriteAllText(storageFile, editBox.Text);
            }
            catch (Exception exc)
            {
                exception = exc.Message;
            }

            if (exception != null)
            {
                string message = String.Format("Could not save file {0}: {1}",
                                               System.IO.Path.GetFileName(storageFile), exception);
                MessageBox.Show(message, "XAML Cruncher");
            }
            else
            {
                editBox.IsModified = false;
                filenameText.Text = storageFile;
            }
        }
        #endregion


        #region ファイルを開く処理

        /// <summary>
        /// 追加ボタン
        /// </summary>
        private async void OnAddAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            // デフォルトXAML表示する
            await CheckIfOkToTrashFile(SetDefaultXamlFile);
        }

        /// <summary>
        /// 開くボタン
        /// </summary>
        private async void OnOpenAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            // ファイルを読み込んで表示
            await CheckIfOkToTrashFile(LoadFileFromOpenPicker);
        }

        private async Task CheckIfOkToTrashFile(Action commandAction)
        {
            // 変更されていなければそのまま実行
            if (!editBox.IsModified)
            {
                commandAction();
                return;
            }

            // 変更されているので現在のXAMLをどうするか確認
            string message =
                String.Format("Do you want to save changes to {0}?",
                    string.IsNullOrEmpty(loadedStorageFile) ? "(untitled)" : System.IO.Path.GetFileName(loadedStorageFile));
            var buttons = new DialogButton[]
            {
                new DialogButton("Save", "save", true),
                new DialogButton("Don't Save", "dont"),
                new DialogButton("Cancel", "cancel", false, true)
            };

            MessageDialog msgdlg = new MessageDialog(message, "XAML Cruncher", buttons);
            object command = await msgdlg.ShowAsync();

            // キャンセル（＝何もしないで終了）
            if ((string)command == "cancel" || command == null)
            {
                return;
            }
            // 保存しない（＝保存せずに読み込みを実行）
            if ((string)command == "dont")
            {
                commandAction();
                return;
            }

            // 保存してから読み込みを実行
            if (string.IsNullOrEmpty(loadedStorageFile))
            {
                string storageFile = GetFileFromSavePicker();
                if (string.IsNullOrEmpty(storageFile))
                {
                    return;
                }
                loadedStorageFile = storageFile;
            }

            SaveXamlToFile(loadedStorageFile);
            commandAction();
        }

        /// <summary>
        /// ダイアログからファイルを指定して読み込む
        /// </summary>
        void LoadFileFromOpenPicker()
        {
            Microsoft.Win32.OpenFileDialog picker = new Microsoft.Win32.OpenFileDialog();
            picker.Filter = "XAML(*.xaml)|*.xaml";
            picker.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string name = fileName;
            if (!string.IsNullOrEmpty(loadedStorageFile))
            {
                name = System.IO.Path.GetFileName(loadedStorageFile);
            }
            picker.FileName = name;
            var pickerResult = picker.ShowDialog();

            if (pickerResult == true)
            {
                string exception = null;
                string file = picker.FileName;

                try
                {
                    editBox.Text = File.ReadAllText(file);
                }
                catch (Exception exc)
                {
                    exception = exc.Message;
                }

                if (exception != null)
                {
                    string message = String.Format("Could not load file {0}: {1}",
                                                   System.IO.Path.GetFileName(file), exception);
                    MessageBox.Show(message, "XAML Cruncher");

                }
                else
                {
                    editBox.IsModified = false;
                    loadedStorageFile = file;
                    filenameText.Text = file;
                }
            }
        }
        #endregion


        /// <summary>
        /// 設定画面の表示
        /// </summary>
        private void OnSettingsAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            settingsDialog.DataContext = appSettings;

            Popup popup = new Popup
            {
                Child = settingsDialog,
                PlacementTarget = (UIElement)sender,
                StaysOpen = false,
            };
            popup.IsOpen = true;
        }

    }
}
