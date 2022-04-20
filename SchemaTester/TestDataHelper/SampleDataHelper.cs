using System.IO;
using System.Text.Json;
using SchemaTester.Data;

namespace SchemaTester.TestDataHelper {
    /// <summary>
    /// Класс-генератор тестовых данных
    /// </summary>
    internal static class SampleDataHelper {
        public static void MakeTestData(string fileName) {
            var scheme = new Schema();
            scheme.AddLine(10, 0, 3).AppendLine(1, 2).AppendLine(2, 0);
            var segment = scheme.AddLine(10, 0, 5).AppendLine(2, 3).AppendLine(4, 3, true);
            segment.AppendLine(49, 3).AppendLine(50, 4, true);
            segment = segment.AppendLine(5, 4, true);
            segment.AppendLine(50, 4, true).AppendLine(53, 4).AppendLine(55, 6, true);
            segment = segment.AppendLine(6, 5, true);
            segment.AppendLine(49, 5).AppendLine(50, 6, true);
            segment = segment.AppendLine(7, 6, true);
            segment.AppendLine(50, 6, true).AppendLine(55, 6, true).AppendLine(56, 6).AppendLine(58, 8, true);
            segment = segment.AppendLine(8, 7, true);
            segment.AppendLine(49, 7).AppendLine(50, 8, true);
            segment = segment.AppendLine(9, 8, true);
            segment.AppendLine(50, 8, true).AppendLine(53, 8, true).AppendLine(58, 8, true).AppendLine(63, 8);
            segment = segment.AppendLine(10, 9, true);
            var segment2 = segment.AppendLine(23, 9, true);
            segment2.AppendLine(52, 9).AppendLine(53, 8, true);
            segment2.AppendLine(24, 10).AppendLine(51, 10, true).AppendLine(52, 9, true);
            segment.AppendLine(14, 13);
            scheme.AddLine(10, 0, 11).AppendLine(10, 11).AppendLine(12, 13);

            scheme.AddFill(10, "Park 1 Full", 4, 3, 49, 3)
                .AppendFill(49, 3, 50, 4)
                .AppendFill(50, 4, 53, 4)
                .AppendFill(53, 4, 55, 6)
                .AppendFill(55, 6, 56, 6)
                .AppendFill(56, 6, 58, 8)
                .AppendFill(58, 8, 53, 8)
                .AppendFill(53, 8, 52, 9)
                .AppendFill(52, 9, 51, 10)
                .AppendFill(51, 10, 24, 10)
                .AppendFill(24, 10, 23, 9)
                .AppendFill(23, 9, 10, 9)
                .AppendFill(10, 9, 9, 8)
                .AppendFill(9, 8, 8, 7)
                .AppendFill(8, 7, 7, 6)
                .AppendFill(7, 6, 6, 5)
                .AppendFill(6, 5, 5, 4)
                .AppendFill(5, 4, 4, 3);

            scheme.AddFill(10, "Park 1 Top", 4, 3, 49, 3)
                .AppendFill(49, 3, 50, 4)
                .AppendFill(50, 4, 53, 4)
                .AppendFill(53, 4, 55, 6)
                .AppendFill(55, 6, 50, 6)
                .AppendFill(50, 6, 49, 5)
                .AppendFill(49, 5, 6, 5)
                .AppendFill(6, 5, 5, 4)
                .AppendFill(5, 4, 4, 3);

            scheme.SaveToFile(fileName);
        }

        public static void MakeTestDataBox(string fileName) {
            var box = new Schema();
            box.AddLine(10, 0, 0, true).AppendLine(1, 0, true).AppendLine(1, 1, true).AppendLine(0, 1, true)
                .AppendLine(0, 0, true);

            box.AddFill(10, "1", 0, 0, 1, 0).AppendFill(1, 0, 1, 1).AppendFill(1, 1, 0, 1).AppendFill(0, 1, 0, 0);

            box.SaveToFile(fileName);
        }

        private static void SaveToFile(this Schema schema, string fileName) {
            using var file = File.Create(fileName);
            JsonSerializer.Serialize(file, schema);
        }
    }
}
