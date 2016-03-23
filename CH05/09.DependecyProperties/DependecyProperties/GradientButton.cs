using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DependecyProperties
{
    public class GradientButton : Button
    {
        private GradientStop gradientStop1, gradientStop2;

        #region 添付プロパティの生成
        public static DependencyProperty Color1Property { private set; get; }
        public static DependencyProperty Color2Property { private set; get; }

        static GradientButton()
        {
            /*
            添付プロパティの作成
             第1引数: プロパティ名
             第2引数: プロパティの型
             第3引数: 依存関係プロパティを登録するクラスの型
             第4引数: PropertyMetadata（プロパティの既定値と値が変化した時に呼び出されるメソッド）
             */
            Color1Property = 
                DependencyProperty.Register("Color1", 
                    typeof(Color), 
                    typeof(GradientButton), 
                    new PropertyMetadata(Colors.White, OnCloseChanged));

            Color2Property =
                DependencyProperty.Register("Color2",
                    typeof(Color),
                    typeof(GradientButton),
                    new PropertyMetadata(Colors.Black, OnCloseChanged));
        }

        #region 更に別の生成方法
        /*
        通常のプロパティと同じように、
        static フィールドを作成しておいてからプロパティに指定することもできる

        static readonly DependencyProperty color1Property = 
            DependencyProperty.Register("Color1",
                typeof(Color),
                typeof(GradientButton),
                new PropertyMetadata(Colors.White, OnCloseChanged));

        static readonly DependencyProperty color2Property =
            DependencyProperty.Register("Color2",
                typeof(Color),
                typeof(GradientButton),
                new PropertyMetadata(Colors.Black, OnCloseChanged));

        public static DependencyProperty Color1Property
        {
            get { return color1Property; }
        }
        public static DependencyProperty Color2Property
        {
            get { return color2Property; }
        }
        */

        /*
        staticフィールドだけで定義することも可能。
        しかしあまり使わないほうがいい。
        この場合は
        ・public static にする必要がある
        ・フィールド名をプロパティ名と同じように大文字から始める

        public static readonly DependencyProperty Color1Property =
            DependencyProperty.Register("Color1",
                typeof(Color),
                typeof(GradientButton),
                new PropertyMetadata(Colors.White, OnCloseChanged));

        public static readonly DependencyProperty Color2Property =
            DependencyProperty.Register("Color2",
                typeof(Color),
                typeof(GradientButton),
                new PropertyMetadata(Colors.Black, OnCloseChanged));
        */
        #endregion

        #endregion


        public GradientButton()
        {
            gradientStop1 = new GradientStop
            {
                Offset = 0,
                Color = this.Color1
            };

            gradientStop2 = new GradientStop
            {
                Offset = 1,
                Color = this.Color2
            };

            LinearGradientBrush brush = new LinearGradientBrush();
            brush.GradientStops.Add(gradientStop1);
            brush.GradientStops.Add(gradientStop2);

            this.Foreground = brush;
        }

        #region 通常のプロパティ
        public Color Color1
        {
            set { SetValue(Color1Property, value); }
            get { return (Color)GetValue(Color1Property); }
        }

        public Color Color2
        {
            set { SetValue(Color2Property, value); }
            get { return (Color)GetValue(Color2Property); }
        }
        #endregion


        static void OnCloseChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as GradientButton).OnCloseChanged(args);
        }

        void OnCloseChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.Property == Color1Property)
            {
                gradientStop1.Color = (Color)args.NewValue;
            }

            if (args.Property == Color2Property)
            {
                gradientStop2.Color = (Color)args.NewValue;
            }
        }
    }
}
