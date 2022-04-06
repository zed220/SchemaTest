using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchemeTester.Controls {
    public class SchemeControl : Control {
        public static readonly DependencyProperty FillColorProperty;
        public static readonly DependencyProperty StrokeColorProperty;
        public static readonly DependencyProperty DotColorProperty;
        public static readonly DependencyProperty PathSchemeProperty;
        public static readonly DependencyProperty PathFillProperty;

        static SchemeControl() {
            FillColorProperty = DependencyProperty.Register(
                nameof(FillColor), typeof(Color), typeof(SchemeControl), new PropertyMetadata(default(Color)));
            StrokeColorProperty = DependencyProperty.Register(
                nameof(StrokeColor), typeof(Color), typeof(SchemeControl), new PropertyMetadata(default(Color)));
            DotColorProperty = DependencyProperty.Register(
                nameof(DotColor), typeof(Color), typeof(SchemeControl), new PropertyMetadata(default(Color)));
            PathSchemeProperty = DependencyProperty.Register(
                nameof(PathScheme), typeof(Geometry), typeof(SchemeControl), new PropertyMetadata(default(Geometry)));
            PathFillProperty = DependencyProperty.Register(
                nameof(PathFill), typeof(Geometry), typeof(SchemeControl), new PropertyMetadata(default(Geometry)));
        }

        public SchemeControl() => DefaultStyleKey = typeof(SchemeControl);

        public Color FillColor {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }
        public Color StrokeColor {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }
        public Color DotColor {
            get => (Color)GetValue(DotColorProperty);
            set => SetValue(DotColorProperty, value);
        }
        public Geometry PathScheme {
            get => (Geometry)GetValue(PathSchemeProperty);
            set => SetValue(PathSchemeProperty, value);
        }
        public Geometry PathFill {
            get => (Geometry)GetValue(PathFillProperty);
            set => SetValue(PathFillProperty, value);
        }
    }
}
