using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using SchemaTester.Data;
using SchemaTester.Models;
using System.Windows;

namespace SchemaTester.Logic {
    public static class PathBuilder {
        private const double FixBlurOffset = 0.5;
        private const string ExceptionDefaultTextFormat = "Исходные данные содержат ошибку. Не найдена исходная линия с Id={0}";

        public static (Geometry lines, IReadOnlyList<FillIModel> fills) ToGeometry(this Schema source) {
            var linesDict = source.LineEnds.ToDictionary(x => x.Id);
            return (lines: GetLinesAndDots(source, linesDict), GetFills(source, linesDict));
        }

        private static Geometry GetLinesAndDots(in Schema source, in Dictionary<int, LineEnd> linesDict) {
            var geometry = new GeometryGroup();
            var addedPoints = new HashSet<Point>();

            foreach (var line in source.LineEnds) {
                var point = line.Point.ToPointAndFixBlur();
                if (line.Highlighted && !addedPoints.Contains(point)) {
                    addedPoints.Add(point);
                    geometry.Children.Add(new EllipseGeometry(line.Point.ToPointAndFixBlur(), 2d, 2d));
                }
                if (line.ParentId == LineEnd.DefaultParentId)
                    continue;
                if (!linesDict.TryGetValue(line.ParentId, out var prevPoint))
                    throw new InvalidOperationException(string.Format(ExceptionDefaultTextFormat, line.ParentId));
                geometry.Children.Add(new LineGeometry(prevPoint.Point.ToPointAndFixBlur(), line.Point.ToPointAndFixBlur()));
            }

            geometry.Freeze();
            return geometry;
        }

        private static IReadOnlyList<FillIModel> GetFills(in Schema source, in Dictionary<int, LineEnd> linesDict) {
            var result = new List<FillIModel>(source.Fills.Count);
            foreach (var fill in source.Fills) {
                var geometry = new PathGeometry();
                result.Add(new() { Name = fill.Key, Path = geometry });
                PathFigure figure = null;
                foreach (var id in fill.Value) {
                    if (!linesDict.TryGetValue(id, out var line))
                        throw new InvalidOperationException(string.Format(ExceptionDefaultTextFormat, id));
                    if (figure == null) {
                        figure = new PathFigure { StartPoint = line.Point.ToPointAndFixBlur(), IsClosed = true };
                        geometry.Figures.Add(figure);
                    }
                    else
                        figure.Segments.Add(new LineSegment { Point = line.Point.ToPointAndFixBlur() });
                }
                geometry.Freeze();
            }

            return result;
        }

        /// <summary>
        /// Конвертирует в <see cref="Point"/> и убирает размытие для линии толщиной 1
        /// </summary>
        public static Point ToPointAndFixBlur(this System.Drawing.PointF point) => new(point.X + FixBlurOffset, point.Y + FixBlurOffset);
    }
}
