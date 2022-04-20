using SchemaTester.Data;
using SchemaTester.TestDataHelper;

namespace SchemaTesterTests {
    internal static class TestData {
        public static Schema Box => MakeBox();

        private static Schema MakeBox() {
            var box = new Schema();
            box.AddLine(1, 0, 0, true).AppendLine(1, 0, true).AppendLine(1, 1, true).AppendLine(0, 1, true)
                .AppendLine(0, 0, true);
            box.AddFill(1, "1", 0, 0, 1, 0).AppendFill(1, 0, 1, 1).AppendFill(1, 1, 0, 1).AppendFill(0, 1, 0, 0);
            return box;
        }
    }
}
