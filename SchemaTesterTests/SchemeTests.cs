using System.Drawing;
using System.Linq;
using System.Windows.Media;
using NUnit.Framework;
using SchemaTester.Data;
using SchemaTester.Logic;

namespace SchemaTesterTests {
    [TestFixture]
    public class BoxTests {
        private Schema _box;

        [OneTimeSetUp]
        public void LoadTestData() {
            _box = TestData.Box;
        }

        [Test]
        public void SchemaTestSimple() {
            var geometry = _box.ToGeometry();
            Assert.IsInstanceOf<GeometryGroup>(geometry.lines);
        }

        [Test]
        public void SchemaTestLines() {
            var geometry = _box.ToGeometry();
            Assert.IsInstanceOf<GeometryGroup>(geometry.lines);
            var group = (GeometryGroup)geometry.lines;
            var lines = group.Children.OfType<LineGeometry>().ToList();
            Assert.AreEqual(4, lines.Count);
            Assert.AreEqual(new PointF(0, 0).ToPointAndFixBlur(), lines[0].StartPoint);
            Assert.AreEqual(new PointF(1, 0).ToPointAndFixBlur(), lines[0].EndPoint);
            Assert.AreEqual(new PointF(1, 0).ToPointAndFixBlur(), lines[1].StartPoint);
            Assert.AreEqual(new PointF(1, 1).ToPointAndFixBlur(), lines[1].EndPoint);
            Assert.AreEqual(new PointF(1, 1).ToPointAndFixBlur(), lines[2].StartPoint);
            Assert.AreEqual(new PointF(0, 1).ToPointAndFixBlur(), lines[2].EndPoint);
            Assert.AreEqual(new PointF(0, 1).ToPointAndFixBlur(), lines[3].StartPoint);
            Assert.AreEqual(new PointF(0, 0).ToPointAndFixBlur(), lines[3].EndPoint);
        }

        [Test]
        public void SchemaTestDots() {
            var geometry = _box.ToGeometry();
            var group = (GeometryGroup)geometry.lines;
            var dots = group.Children.OfType<EllipseGeometry>().ToList();
            Assert.AreEqual(4, dots.Count);
            foreach (var dot in dots) {
                Assert.AreEqual(2, dot.RadiusX);
                Assert.AreEqual(2, dot.RadiusY);
            }
            Assert.AreEqual(new PointF(0, 0).ToPointAndFixBlur(), dots[0].Center);
            Assert.AreEqual(new PointF(1, 0).ToPointAndFixBlur(), dots[1].Center);
            Assert.AreEqual(new PointF(1, 1).ToPointAndFixBlur(), dots[2].Center);
            Assert.AreEqual(new PointF(0, 1).ToPointAndFixBlur(), dots[3].Center);
        }

        [Test]
        public void SchemaTestFill() {
            var geometry = _box.ToGeometry();
            Assert.AreEqual(1, geometry.fills.Count);
            var fillModel = geometry.fills.Single(x => x.Name == "1");
            Assert.IsInstanceOf<PathGeometry>(fillModel.Path);
            var fill = (PathGeometry)fillModel.Path;
            Assert.AreEqual(1, fill.Figures.Count);
            var figure = fill.Figures.Single();
            Assert.True(figure.IsClosed);
            Assert.AreEqual(3, figure.Segments.Count);
            var lineSegments = figure.Segments.OfType<LineSegment>().ToList();
            Assert.AreEqual(3, lineSegments.Count);
            Assert.AreEqual(new PointF(1, 0).ToPointAndFixBlur(), figure.StartPoint);
            Assert.AreEqual(new PointF(1, 1).ToPointAndFixBlur(), lineSegments[0].Point);
            Assert.AreEqual(new PointF(0, 1).ToPointAndFixBlur(), lineSegments[1].Point);
            Assert.AreEqual(new PointF(0, 0).ToPointAndFixBlur(), lineSegments[2].Point);
        }
    }
}
