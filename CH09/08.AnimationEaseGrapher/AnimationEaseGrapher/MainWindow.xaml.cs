using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AnimationEaseGrapher
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private EasingFunctionBase easingFunction;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnMainWindowLoaded;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Type baseType = typeof(EasingFunctionBase);
            TypeInfo baseTypeInfo = baseType.GetTypeInfo();
            Assembly assembly = baseTypeInfo.Assembly;

            // EasingFunctionBase のすべての派生クラスを列挙
            foreach (Type type in assembly.ExportedTypes)
            {
                TypeInfo typeInfo = type.GetTypeInfo();

                // イージング関数ごとにRadioButtonを作成
                if (typeInfo.IsPublic && 
                    baseTypeInfo.IsAssignableFrom(typeInfo) && 
                    type != baseType)
                {
                    RadioButton radioButton = new RadioButton
                    {
                        Content = type.Name,
                        Tag = type,
                        Margin = new Thickness(6),
                    };
                    radioButton.Checked += OnEasingFunctionRadioButtonChecked;
                    easingFunctionStackPanel.Children.Add(radioButton);
                }
            }

            // StackPanelの最初のRadioButton("None")をチェック
            (easingFunctionStackPanel.Children[0] as RadioButton).IsChecked = true;
        }

        private void OnEasingFunctionRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            Type type = radioButton.Tag as Type;
            easingFunction = null;
            propertiesStackPanel.Children.Clear();

            // typeがnullになるのは"None"ボタンの場合のみ
            if (type != null)
            {
                TypeInfo typeInfo = type.GetTypeInfo();

                // パラメータなしのコンストラクタを取得し、イージング関数をインスタンス化
                foreach (ConstructorInfo constructorInfo in typeInfo.DeclaredConstructors)
                {
                    if (constructorInfo.IsPublic && constructorInfo.GetParameters().Length == 0)
                    {
                        easingFunction = constructorInfo.Invoke(null) as EasingFunctionBase;
                        break;
                    }
                }

                // イージング関数のプロパティを列挙
                foreach (PropertyInfo property in typeInfo.DeclaredProperties)
                {
                    // int型とdouble型のプロパティだけを処理
                    if (property.PropertyType != typeof(int) && 
                        property.PropertyType != typeof(double))
                    {
                        continue;
                    }

                    // プロパティ名を表示するためのTextBlockを作成
                    TextBlock txtblk = new TextBlock
                    {
                        Text = property.Name + ":",
                    };
                    propertiesStackPanel.Children.Add(txtblk);

                    // プロパティ値を表すSlider
                    Slider slider = new Slider
                    {
                        Width = 144,
                        Minimum = 0,
                        Maximum = 10,
                        Tag = property,
                    };

                    if (property.PropertyType == typeof(int))
                    {
                        slider.SmallChange = 1;
                        slider.Value = (int)property.GetValue(easingFunction);
                    }
                    else
                    {
                        slider.SmallChange = 0.1;
                        slider.Value = (double)property.GetValue(easingFunction);
                    }

                    // Sliderのイベントハンドラを定義
                    slider.ValueChanged += (sliderSender, sliderArgs) =>
                    {
                        Slider sliderChanging = sliderSender as Slider;
                        PropertyInfo propertyInfo = sliderChanging.Tag as PropertyInfo;

                        if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(easingFunction, (int)sliderArgs.NewValue);
                        }
                        else
                        {
                            property.SetValue(easingFunction, (double)sliderArgs.NewValue);
                        }
                        DrawNewGraph();
                    };
                    propertiesStackPanel.Children.Add(slider);
                }
            }

            // EasingModeを表すRadioButtonを初期化
            foreach (UIElement child in easingModeStackPanel.Children)
            {
                RadioButton easingModeRadioButton = child as RadioButton;
                easingModeRadioButton.IsEnabled = easingFunction != null;

                easingModeRadioButton.IsChecked = easingFunction != null &&
                    easingFunction.EasingMode == (EasingMode)easingModeRadioButton.Tag;
            }

            DrawNewGraph();
        }

        private void OnEasingModeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            easingFunction.EasingMode = (EasingMode)radioButton.Tag;
            DrawNewGraph();
        }

        private void OnDemoButtonClick(object sender, RoutedEventArgs e)
        {
            // 選択されたイージング関数を設定し、アニメーションを開始
            Storyboard storyboard = this.Resources["storyboard"] as Storyboard;
            (storyboard.Children[1] as DoubleAnimation).EasingFunction = easingFunction;
            storyboard.Begin();
        }

        private void DrawNewGraph()
        {
            polyline.Points.Clear();
            if (easingFunction == null)
            {
                polyline.Points.Add(new Point(0, 0));
                polyline.Points.Add(new Point(1000, 500));
                return;
            }

            for (decimal t = 0; t <= 1; t += 0.01m)
            {
                double x = (double)(1000 * t);
                double y = 500 * easingFunction.Ease((double)t);
                polyline.Points.Add(new Point(x, y));
            }
        }
    }
}
