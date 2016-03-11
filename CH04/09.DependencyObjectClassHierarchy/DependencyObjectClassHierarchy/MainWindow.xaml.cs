using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace DependencyObjectClassHierarchy
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Type rootType = typeof(DependencyObject);
        TypeInfo rootTypeInfo = typeof(DependencyObject).GetTypeInfo();
        List<Type> classes = new List<Type>();
        Brush highlightBrush;

        public MainWindow()
        {
            InitializeComponent();

            MainLogic();
        }

        private void MainLogic()
        {
            //highlightBrush = new SolidColorBrush(new UISettings().UIElementColor(UIElementType.Highlight));
            highlightBrush = new SolidColorBrush(SystemColors.HighlightColor);

            // DependencyObjectを継承しているすべてのクラスを追加
            // WPFではDependencyObjectのみでは取得できない
            AddToClassList(typeof(System.Windows.DependencyObject));
            AddToClassList(typeof(System.Windows.UIElement));
            AddToClassList(typeof(System.Windows.Window));

            // それらを名前のアルファベット順にソート
            classes.Sort((t1, t2) =>
            {
                return String.Compare(t1.GetTypeInfo().Name, t2.GetTypeInfo().Name);
            });

            // ソート済みのクラスをすべてツリー構造に追加
            ClassAndSubclasses rootClass = new ClassAndSubclasses(rootType);
            AddToTree(rootClass, classes);

            // StackPanelに追加されたTextBlockを使ってツリーを表示
            Display(rootClass, 0);
        }

        /// <summary>
        /// Typeを指定してそれ以下の継承ツリーのクラスをclassesに追加する
        /// </summary>
        private void AddToClassList(Type sampleType)
        {
            Assembly assembly = sampleType.GetTypeInfo().Assembly;
            foreach (Type type in assembly.ExportedTypes)
            {
                TypeInfo typeinfo = type.GetTypeInfo();
                // X.IsAssignableFrom(Y)  => Y : X な関係。
                // X は Y から継承されている => Y は X を継承している
                if (typeinfo.IsPublic && rootTypeInfo.IsAssignableFrom(typeinfo))
                {
                    classes.Add(type);
                }
            }
        }

        /// <summary>
        /// classes をループして、parentClassの直接のサブクラスを追加する
        /// </summary>
        private void AddToTree(ClassAndSubclasses parentClass, List<Type> classes)
        {
            foreach (Type type in classes)
            {
                Type baseType = type.GetTypeInfo().BaseType;

                // parentClassが直接の親だったらサブクラスに追加
                if (baseType == parentClass.Type)
                {
                    ClassAndSubclasses subClass = new ClassAndSubclasses(type);
                    parentClass.Subclasses.Add(subClass);
                    AddToTree(subClass, classes);
                }
            }
        }

        private void Display(ClassAndSubclasses parentClass, int indent)
        {
            TypeInfo typeInfo = parentClass.Type.GetTypeInfo();

            // TextBlockを作成し、型の名前を設定
            TextBlock txtblk = new TextBlock();
            txtblk.Inlines.Add(new Run() { Text = new string(' ', 8 * indent) });
            txtblk.Inlines.Add(new Run() { Text = typeInfo.Name });

            // sealed(継承不可)クラスかどうかをチェック
            if (typeInfo.IsSealed)
            {
                txtblk.Inlines.Add(new Run()
                    {
                        Text = " (sealed)",
                        Foreground = highlightBrush
                    });
            }

            // クラスがインスタンス化可能かどうかをチェック
            IEnumerable<ConstructorInfo> constructorInfos = typeInfo.DeclaredConstructors;
            bool existsPublicConstructor = false;
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                if (constructorInfo.IsPublic)
                {
                    existsPublicConstructor = true;
                    break;
                }
            }

            if (existsPublicConstructor == false)
            {
                txtblk.Inlines.Add(new Run()
                {
                    Text = " (non-instantiable)",
                    Foreground = highlightBrush
                });
            }

            // StackPanelに追加
            stackPanel.Children.Add(txtblk);

            // このメソッドを全てのサブクラスで再帰的に呼び出す
            foreach (ClassAndSubclasses sublcass in parentClass.Subclasses)
            {
                Display(sublcass, indent + 1);
            }
        }
    }

    public class ClassAndSubclasses
    {
        public ClassAndSubclasses(Type parent)
        {
            this.Type = parent;
            this.Subclasses = new List<ClassAndSubclasses>();
        }

        public Type Type { protected set; get; }
        public List<ClassAndSubclasses> Subclasses { protected set; get; }
    }
}
