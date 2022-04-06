using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchemeTesterTests {
    internal static class Helper {
        public static Point FixPoint(this Point source) => new(source.X + 0.5d, source.Y + 0.5d);
    }
}
