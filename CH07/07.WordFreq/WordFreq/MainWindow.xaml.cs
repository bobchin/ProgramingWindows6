using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WordFreq
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // ハーマン・メルヴィルの「白鯨」の電子書籍バージョン
        Uri uri = new Uri("http://www.gutenberg.org/ebooks/2701.txt.utf-8");
        CancellationTokenSource cts;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            progressBar.Value = 0;
            errorText.Text = "";
            startButton.IsEnabled = false;

            Task startTask = new Task(() => StartTask());
            startTask.Start();
        }

        private async void StartTask()
        {
            IOrderedEnumerable<KeyValuePair<string, int>> wordList = null;

            try
            {
                HttpClient client = new HttpClient();
                await Dispatcher.InvokeAsync(() =>
                {
                    cancleButton.IsEnabled = true;
                }, DispatcherPriority.Background);

                cts = new CancellationTokenSource();
                var response = await client.GetAsync(uri, cts.Token);

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    //cancleButton.IsEnabled = true;
                    //cts = new CancellationTokenSource();
                    wordList = await GetWordFrequenciesAsync(stream, cts.Token, new Progress<double>(ProgressCallback));
                    if (wordList != null || wordList.Count() == 0)
                    {
                        await Dispatcher.InvokeAsync(() =>
                        {
                            cancleButton.IsEnabled = false;
                        }, DispatcherPriority.Background);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                DispatcherOperation t3 = Dispatcher.InvokeAsync(() =>
                {
                    progressBar.Value = 0;
                    cancleButton.IsEnabled = false;
                    startButton.IsEnabled = true;
                }, DispatcherPriority.Background);
            }
            catch (Exception exc)
            {
                DispatcherOperation t4 = Dispatcher.InvokeAsync(() =>
                {
                    progressBar.Value = 0;
                    cancleButton.IsEnabled = false;
                    startButton.IsEnabled = true;
                    errorText.Text = "Error: " + exc.Message;
                }, DispatcherPriority.Background);
                return;
            }

            // 単語とカウントのリストをStackPanelへ転送
            foreach (KeyValuePair<string, int> word in wordList)
            {
                if (word.Value > 1)
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        TextBlock txtblk = new TextBlock
                        {
                            FontSize = 24,
                            Text = word.Key + " \x2014" + word.Value.ToString(),
                        };
                        stackPanel.Children.Add(txtblk);
                    }, DispatcherPriority.Background);
                }
                await Task.Yield();
            }

            if (wordList != null)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    startButton.IsEnabled = true;
                }, DispatcherPriority.Normal);
            }
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        private void ProgressCallback(double progress)
        {
            Dispatcher.InvokeAsync(() =>
            {
                progressBar.Value = progress;
            }, DispatcherPriority.Background);
        }

        Task<IOrderedEnumerable<KeyValuePair<string, int>>> GetWordFrequenciesAsync(
            Stream stream,
            CancellationToken cancellationToken,
            IProgress<double> progress)
        {
            return Task.Run(async () =>
            {

                Dictionary<string, int> dictionary = new Dictionary<string, int>();

                using (StreamReader streamReader = new StreamReader(stream))
                {
                    // １行目を読み取る
                    string line = await streamReader.ReadLineAsync();

                    while (line != null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        progress.Report(100.0 * stream.Position / stream.Length);

                        string[] words = line.Split(' ', ',', '.', ';', ':');

                        foreach (string word in words)
                        {
                            string charWord = word.ToLower();

                            // 前後のUnicode以外の文字を削除
                            while (charWord.Length > 0 && !Char.IsLetter(charWord[0]))
                            {
                                charWord = charWord.Substring(1);
                            }
                            while (charWord.Length > 0 && !Char.IsLetter(charWord[charWord.Length - 1]))
                            {
                                charWord = charWord.Substring(0, charWord.Length - 1);
                            }

                            if (charWord.Length == 0)
                            {
                                continue;
                            }

                            if (dictionary.ContainsKey(charWord))
                            {
                                dictionary[charWord] += 1;
                            }
                            else
                            {
                                dictionary.Add(charWord, 1);
                            }
                        }
                        line = await streamReader.ReadLineAsync();
                    }
                }

                // Value（単語の登録回数）でソートしたディクショナリを返す
                return dictionary.OrderByDescending(i => i.Value);

            }, cancellationToken);
        }
    }
}
