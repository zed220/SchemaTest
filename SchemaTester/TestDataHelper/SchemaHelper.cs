using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SchemaTester.Data;

namespace SchemaTester.TestDataHelper{
    public static class SchemaHelper {
        public static (Schema, LineEnd, int) AddLine(this Schema target, int scale, float x, float y, bool highlighted = false) {
            var newSegment = new LineEnd { Id = target.LineEnds.Count, ParentId = -1, Point = CreatePoint(x, y, scale), Highlighted = highlighted };
            target.LineEnds.Add(newSegment);
            return (target, newSegment, scale);
        }
        public static (Schema schema, LineEnd line, int scale) AppendLine(this (Schema schema, LineEnd line, int scale) target, float x, float y, bool highlighted = false) {
            var newSegment = new LineEnd { Id = target.Item1.LineEnds.Count, ParentId = target.Item2.Id, Point = CreatePoint(x, y, target.scale), Highlighted = highlighted };
            target.Item1.LineEnds.Add(newSegment);
            return (target.schema, newSegment, target.scale);
        }

        public static (Schema schema, string fillName, int scale) AddFill(this Schema target, int scale, string fillName, float x1, float y1, float x2, float y2) {
            if (!target.Fills.TryGetValue(fillName, out var ids))
                ids = target.Fills[fillName] = new();
            ids.Add(target.GetIdByLine(scale, x1, y1, x2, y2));
            return (target, fillName, scale);
        }
        public static (Schema schema, string fillName, int scale) AppendFill(this (Schema schema, string fillName, int scale) target, float x1, float y1, float x2, float y2) {
            target.schema.Fills[target.fillName].Add(target.schema.GetIdByLine(target.scale, x1, y1, x2, y2));
            return target;
        }

        private static int GetIdByLine(this Schema target, int scale, float x1, float y1, float x2, float y2) {
            var p1 = CreatePoint(x1, y1, scale);
            var p2 = CreatePoint(x2, y2, scale);
            foreach (var segment in target.LineEnds) {
                if (segment.Point == p1) {
                    if (target.LineEnds.FirstOrDefault(s => s.ParentId == segment.Id && s.Point == p2) is { } s)
                        return s.Id;
                    if (target.LineEnds.FirstOrDefault(s => s.Id == segment.ParentId && s.Point == p2) is { })
                        return segment.Id;
                }
                if (segment.Point == p2) {
                    if (target.LineEnds.FirstOrDefault(s => s.ParentId == segment.Id && s.Point == p1) is { })
                        return segment.Id;
                    if (target.LineEnds.FirstOrDefault(s => s.Id == segment.ParentId && s.Point == p1) is { } s2)
                        return s2.Id;
                }
            }

            throw new InvalidOperationException();
        }

        private static PointF CreatePoint(float x, float y, int scale) => new(x * scale, y * scale);
    }
}