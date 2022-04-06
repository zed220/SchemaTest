using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SchemeTester.Data;

namespace SchemeTester.Logic {
    public static class PathBuilder {
        public static int Scale { get; set; } = 10;

        public static Geometry DataToGeometry(Scheme source) {
            var geometry = new GeometryGroup();
            var dots = new HashSet<(float, float)>();
            foreach (var segment in source.Segments) {
                var point = (segment.X, segment.Y);
                if (segment.Highlighted && !dots.Contains(point)) {
                    dots.Add(point);
                    geometry.Children.Add(new EllipseGeometry(segment.ToPoint(), 2d, 2d));
                }
                if (segment.ParentId < 0)
                    continue;
                var prevPoint = source.Segments.First(x => x.Id == segment.ParentId);
                geometry.Children.Add(new LineGeometry(prevPoint.ToPoint(), segment.ToPoint()));
            }

            geometry.Freeze();
            return geometry;
        }

        public static Dictionary<string, Geometry> DataToGeometryFill(Scheme source) {
            var result = new Dictionary<string, Geometry>();
            foreach (var fill in source.Fills) {
                var geometry = new PathGeometry();
                result.Add(fill.Key, geometry);
                PathFigure figure = null;
                foreach (var id in fill.Value) {
                    var currentSegment = source.Segments.First(x => x.Id == id);
                    if (figure == null) {
                        figure = new PathFigure { StartPoint = currentSegment.ToPoint(), IsClosed = true };
                        geometry.Figures.Add(figure);
                        continue;
                    }

                    figure.Segments.Add(new LineSegment { Point = currentSegment.ToPoint() });
                }
                geometry.Freeze();
            }

            return result;
        }

        /// <summary>
        /// Масштабирует и убирает размытие для линии толщиной 1
        /// </summary>
        private static Point ToPoint(this Segment source) => new(source.X * Scale + 0.5d, source.Y * Scale + 0.5d);
    }
}
