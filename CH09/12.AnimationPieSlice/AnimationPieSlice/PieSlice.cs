using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AnimationPieSlice
{
    public class PieSlice : Shape
    {
        PathFigure pathFigure;
        LineSegment lineSegment;
        ArcSegment arcSegment;

        static PieSlice()
        {
            /*
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Path), 
                new FrameworkPropertyMetadata(typeof(Path)));
            */
            CenterProperty = DependencyProperty.Register("Center",
                typeof(Point), typeof(PieSlice),
                new PropertyMetadata(new Point(100, 100), OnPropertyChanged));

            RadiusProperty = DependencyProperty.Register("Radius",
                typeof(double), typeof(PieSlice),
                new PropertyMetadata(100.0, OnPropertyChanged));

            StartAngleProperty = DependencyProperty.Register("StartAngle",
                typeof(double), typeof(PieSlice),
                new PropertyMetadata(0.0, OnPropertyChanged));

            SweepAngleProperty = DependencyProperty.Register("SweepAngle",
                typeof(double), typeof(PieSlice),
                new PropertyMetadata(90.0, OnPropertyChanged));

            DataProperty = DependencyProperty.Register("Data",
                typeof(Geometry), typeof(PieSlice),
                new PropertyMetadata(null));
        }

        public static DependencyProperty CenterProperty { private set; get; }
        public static DependencyProperty RadiusProperty { private set; get; }
        public static DependencyProperty StartAngleProperty { private set; get; }
        public static DependencyProperty SweepAngleProperty { private set; get; }
        public static DependencyProperty DataProperty { set; get; }

        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as PieSlice).UpdateValues();
        }


        public PieSlice()
        {
            pathFigure = new PathFigure { IsClosed = true };
            lineSegment = new LineSegment();
            arcSegment = new ArcSegment { SweepDirection = SweepDirection.Clockwise };
            pathFigure.Segments.Add(lineSegment);
            pathFigure.Segments.Add(arcSegment);

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            this.Data = pathGeometry;
            UpdateValues();
        }

        public Point Center
        {
            set { SetValue(CenterProperty, value); }
            get { return (Point)GetValue(CenterProperty); }
        }

        public double Radius
        {
            set { SetValue(RadiusProperty, value); }
            get { return (double)GetValue(RadiusProperty); }
        }

        public double StartAngle
        {
            set { SetValue(StartAngleProperty, value); }
            get { return (double)GetValue(StartAngleProperty); }
        }

        public double SweepAngle
        {
            set { SetValue(SweepAngleProperty, value); }
            get { return (double)GetValue(SweepAngleProperty); }
        }

        private void UpdateValues()
        {
            pathFigure.StartPoint = this.Center;

            double x = this.Center.X + this.Radius * Math.Sin(Math.PI * this.StartAngle / 180);
            double y = this.Center.Y - this.Radius * Math.Cos(Math.PI * this.StartAngle / 180);
            lineSegment.Point = new Point(x, y);

            x = this.Center.X + this.Radius * Math.Sin(Math.PI * (this.StartAngle + this.SweepAngle) / 180);
            y = this.Center.Y - this.Radius * Math.Cos(Math.PI * (this.StartAngle + this.SweepAngle) / 180);
            arcSegment.Point = new Point(x, y);
            arcSegment.IsLargeArc = this.SweepAngle >= 180;
            arcSegment.Size = new Size(this.Radius, this.Radius);
        }


        // Shape を継承するためのメソッド
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                Geometry data = this.Data;
                if (data == null)
                {
                    data = Geometry.Empty;
                }
                return data;
            }
        }
    }
}
