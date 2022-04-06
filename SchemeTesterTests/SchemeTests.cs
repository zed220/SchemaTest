using System.Linq;
using System.Windows;
using System.Windows.Media;
using NUnit.Framework;
using SchemeTester.Data;
using SchemeTester.Logic;

namespace SchemeTesterTests {
    [TestFixture]
    public class BoxTests {
        private Scheme _box;

        [OneTimeSetUp]
        public void LoadTestData() {
            PathBuilder.Scale = 1;
            _box = TestData.Box;
        }

        [Test]
        public void SchemeTestSimple() {
            var geometry = PathBuilder.DataToGeometry(_box);
            Assert.IsInstanceOf<GeometryGroup>(geometry);
        }

        [Test]
        public void SchemeTestLines() {
            var geometry = PathBuilder.DataToGeometry(_box);
            Assert.IsInstanceOf<GeometryGroup>(geometry);
            var group = (GeometryGroup)geometry;
            var lines = group.Children.OfType<LineGeometry>().ToList();
            Assert.AreEqual(4, lines.Count);
            Assert.AreEqual(new Point(0, 0).FixPoint(), lines[0].StartPoint);
            Assert.AreEqual(new Point(1, 0).FixPoint(), lines[0].EndPoint);
            Assert.AreEqual(new Point(1, 0).FixPoint(), lines[1].StartPoint);
            Assert.AreEqual(new Point(1, 1).FixPoint(), lines[1].EndPoint);
            Assert.AreEqual(new Point(1, 1).FixPoint(), lines[2].StartPoint);
            Assert.AreEqual(new Point(0, 1).FixPoint(), lines[2].EndPoint);
            Assert.AreEqual(new Point(0, 1).FixPoint(), lines[3].StartPoint);
            Assert.AreEqual(new Point(0, 0).FixPoint(), lines[3].EndPoint);
        }

        [Test]
        public void SchemeTestDots() {
            var geometry = PathBuilder.DataToGeometry(_box);
            var group = (GeometryGroup)geometry;
            var dots = group.Children.OfType<EllipseGeometry>().ToList();
            Assert.AreEqual(4, dots.Count);
            foreach (var dot in dots) {
                Assert.AreEqual(2, dot.RadiusX);
                Assert.AreEqual(2, dot.RadiusY);
            }
            Assert.AreEqual(new Point(0, 0).FixPoint(), dots[0].Center);
            Assert.AreEqual(new Point(1, 0).FixPoint(), dots[1].Center);
            Assert.AreEqual(new Point(1, 1).FixPoint(), dots[2].Center);
            Assert.AreEqual(new Point(0, 1).FixPoint(), dots[3].Center);
        }

        [Test]
        public void SchemeTestFill()
        {
            var fills = PathBuilder.DataToGeometryFill(_box);
            Assert.AreEqual(1, fills.Count);
            var geometry = fills["1"];
            Assert.IsInstanceOf<PathGeometry>(geometry);
            var fill = (PathGeometry)geometry;
            Assert.AreEqual(1, fill.Figures.Count);
            var figure = fill.Figures.Single();
            Assert.True(figure.IsClosed);
            Assert.AreEqual(3, figure.Segments.Count);
            var lineSegments = figure.Segments.OfType<LineSegment>().ToList();
            Assert.AreEqual(3, lineSegments.Count);
            Assert.AreEqual(new Point(1, 0).FixPoint(), figure.StartPoint);
            Assert.AreEqual(new Point(1, 1).FixPoint(), lineSegments[0].Point);
            Assert.AreEqual(new Point(0, 1).FixPoint(), lineSegments[1].Point);
            Assert.AreEqual(new Point(0, 0).FixPoint(), lineSegments[2].Point);
        }
    }
}
