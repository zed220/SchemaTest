using SchemeTester.Data;
using SchemeTester.TestDataHelper;

namespace SchemeTesterTests {
    internal static class TestData {
        public static Scheme Box => MakeBox();

        private static Scheme MakeBox() {
            var box = new Scheme();
            box.AddSegment(0, 0, true).AppendSegment(1, 0, true).AppendSegment(1, 1, true).AppendSegment(0, 1, true)
                .AppendSegment(0, 0, true);
            box.AddFill("1", 0, 0, 1, 0).AddFill("1", 1, 0, 1, 1).AddFill("1", 1, 1, 0, 1).AddFill("1", 0, 1, 0, 0);
            return box;
        }
    }
}
