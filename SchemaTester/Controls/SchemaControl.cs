using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchemaTester.Controls {
    public class SchemaControl : Control {
        public static readonly DependencyProperty FillColorProperty;
        public static readonly DependencyProperty StrokeColorProperty;
        public static readonly DependencyProperty PointColorProperty;
        public static readonly DependencyProperty PathSchemaProperty;
        public static readonly DependencyProperty PathFillProperty;

        static SchemaControl() {
            FillColorProperty = DependencyProperty.Register(
                nameof(FillColor), typeof(Color), typeof(SchemaControl), new PropertyMetadata(default(Color)));
            StrokeColorProperty = DependencyProperty.Register(
                nameof(StrokeColor), typeof(Color), typeof(SchemaControl), new PropertyMetadata(default(Color)));
            PointColorProperty = DependencyProperty.Register(
                nameof(PointColor), typeof(Color), typeof(SchemaControl), new PropertyMetadata(default(Color)));
            PathSchemaProperty = DependencyProperty.Register(
                nameof(PathSchema), typeof(Geometry), typeof(SchemaControl), new PropertyMetadata(default(Geometry)));
            PathFillProperty = DependencyProperty.Register(
                nameof(PathFill), typeof(Geometry), typeof(SchemaControl), new PropertyMetadata(default(Geometry)));
        }

        public SchemaControl() => DefaultStyleKey = typeof(SchemaControl);

        public Color FillColor {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }
        public Color StrokeColor {
            get => (Color)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }
        public Color PointColor {
            get => (Color)GetValue(PointColorProperty);
            set => SetValue(PointColorProperty, value);
        }
        public Geometry PathSchema {
            get => (Geometry)GetValue(PathSchemaProperty);
            set => SetValue(PathSchemaProperty, value);
        }
        public Geometry PathFill {
            get => (Geometry)GetValue(PathFillProperty);
            set => SetValue(PathFillProperty, value);
        }
    }
}
