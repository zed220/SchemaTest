using System;
using System.Linq;
using SchemeTester.Data;

namespace SchemeTester.TestDataHelper
{
    public static class SchemeHelper {
        public static (Scheme, Segment) AddSegment(this Scheme target, float x, float y, bool highlighted = false) {
            var newSegment = new Segment { Id = target.Segments.Count, ParentId = -1, X = x, Y = y, Highlighted = highlighted };
            target.Segments.Add(newSegment);
            return (target, newSegment);
        }
        public static (Scheme, Segment) AppendSegment(this (Scheme, Segment) source, float x, float y, bool highlighted = false) {
            var newSegment = new Segment { Id = source.Item1.Segments.Count, ParentId = source.Item2.Id, X = x, Y = y, Highlighted = highlighted };
            source.Item1.Segments.Add(newSegment);
            return (source.Item1, newSegment);
        }

        public static Scheme AddFill(this Scheme target, string name, float x1, float y1, float x2, float y2) {
            if (!target.Fills.TryGetValue(name, out var ids))
                ids = target.Fills[name] = new();
            ids.Add(target.GetIdByLine(x1, y1, x2, y2));
            return target;
        }

        private static int GetIdByLine(this Scheme target, float x1, float y1, float x2, float y2) {
            foreach (var segment in target.Segments) {
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (segment.X == x1 && segment.Y == y1) {
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                    if (target.Segments.FirstOrDefault(s => s.ParentId == segment.Id && s.X == x2 && s.Y == y2) is var s && s != null)
                        return s.Id;
                    if (target.Segments.FirstOrDefault(s => s.Id == segment.ParentId && s.X == x2 && s.Y == y2) is var s2 && s2 != null)
                        return segment.Id;
                }
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (segment.X == x2 && segment.Y == y2) {
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                    if (target.Segments.FirstOrDefault(s => s.ParentId == segment.Id && s.X == x1 && s.Y == y1) is var s && s != null)
                        return segment.Id;
                    if (target.Segments.FirstOrDefault(s => s.Id == segment.ParentId && s.X == x1 && s.Y == y1) is var s2 && s2 != null)
                        return s2.Id;
                }
            }

            throw new InvalidOperationException();
        }
    }
}