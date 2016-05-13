using Windows.UI.Xaml.Controls;
using DirectXWrapper;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace EnumerateFonts
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            WriteFactory writeFactory = new WriteFactory();
            WriteFontCollection writeFontCollection =
                        writeFactory.GetSystemFontCollection();

            int count = writeFontCollection.GetFontFamilyCount();
            string[] fonts = new string[count];

            for (int i = 0; i < count; i++)
            {
                WriteFontFamily writeFontFamily =
                            writeFontCollection.GetFontFamily(i);

                WriteLocalizedStrings writeLocalizedStrings =
                            writeFontFamily.GetFamilyNames();
                int index;

                if (writeLocalizedStrings.FindLocaleName("en-us", out index))
                {
                    fonts[i] = writeLocalizedStrings.GetString(index);
                }
                else
                {
                    fonts[i] = writeLocalizedStrings.GetString(0);
                }
            }
            lstbox.ItemsSource = fonts;

        }
    }
}
