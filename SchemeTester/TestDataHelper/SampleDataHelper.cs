using System.IO;
using Newtonsoft.Json;
using SchemeTester.Data;

namespace SchemeTester.TestDataHelper {
    /// <summary>
    /// Класс-генератор тестовых данных
    /// </summary>
    internal static class SampleDataHelper {
        public static void MakeTestData(string fileName) {
            var scheme = new Scheme();
            scheme.AddSegment(0, 3).AppendSegment(1, 2).AppendSegment(2, 0);
            var segment = scheme.AddSegment(0, 5).AppendSegment(2, 3).AppendSegment(4, 3, true);
            segment.AppendSegment(49, 3).AppendSegment(50, 4, true);
            segment = segment.AppendSegment(5, 4, true);
            segment.AppendSegment(50, 4, true).AppendSegment(53, 4).AppendSegment(55, 6, true);
            segment = segment.AppendSegment(6, 5, true);
            segment.AppendSegment(49, 5).AppendSegment(50, 6, true);
            segment = segment.AppendSegment(7, 6, true);
            segment.AppendSegment(50, 6, true).AppendSegment(55, 6, true).AppendSegment(56, 6).AppendSegment(58, 8, true);
            segment = segment.AppendSegment(8, 7, true);
            segment.AppendSegment(49, 7).AppendSegment(50, 8, true);
            segment = segment.AppendSegment(9, 8, true);
            segment.AppendSegment(50, 8, true).AppendSegment(53, 8, true).AppendSegment(58, 8, true).AppendSegment(63, 8);
            segment = segment.AppendSegment(10, 9, true);
            var segment2 = segment.AppendSegment(23, 9, true);
            segment2.AppendSegment(52, 9).AppendSegment(53, 8, true);
            segment2.AppendSegment(24, 10).AppendSegment(51, 10, true).AppendSegment(52, 9, true);
            segment.AppendSegment(14, 13);
            scheme.AddSegment(0, 11).AppendSegment(10, 11).AppendSegment(12, 13);

            scheme.AddFill("Park 1 Full", 4, 3, 49, 3)
                .AddFill("Park 1 Full", 49, 3, 50, 4)
                .AddFill("Park 1 Full", 50, 4, 53, 4)
                .AddFill("Park 1 Full", 53, 4, 55, 6)
                .AddFill("Park 1 Full", 55, 6, 56, 6)
                .AddFill("Park 1 Full", 56, 6, 58, 8)
                .AddFill("Park 1 Full", 58, 8, 53, 8)
                .AddFill("Park 1 Full", 53, 8, 52, 9)
                .AddFill("Park 1 Full", 52, 9, 51, 10)
                .AddFill("Park 1 Full", 51, 10, 24, 10)
                .AddFill("Park 1 Full", 24, 10, 23, 9)
                .AddFill("Park 1 Full", 23, 9, 10, 9)
                .AddFill("Park 1 Full", 10, 9, 9, 8)
                .AddFill("Park 1 Full", 9, 8, 8, 7)
                .AddFill("Park 1 Full", 8, 7, 7, 6)
                .AddFill("Park 1 Full", 7, 6, 6, 5)
                .AddFill("Park 1 Full", 6, 5, 5, 4)
                .AddFill("Park 1 Full", 5, 4, 4, 3);

            scheme.AddFill("Park 1 Top", 4, 3, 49, 3)
                .AddFill("Park 1 Top", 49, 3, 50, 4)
                .AddFill("Park 1 Top", 50, 4, 53, 4)
                .AddFill("Park 1 Top", 53, 4, 55, 6)
                .AddFill("Park 1 Top", 55, 6, 50, 6)
                .AddFill("Park 1 Top", 50, 6, 49, 5)
                .AddFill("Park 1 Top", 49, 5, 6, 5)
                .AddFill("Park 1 Top", 6, 5, 5, 4)
                .AddFill("Park 1 Top", 5, 4, 4, 3);

            scheme.SaveToFile(fileName);
        }

        public static void MakeTestDataBox(string fileName) {
            var box = new Scheme();
            box.AddSegment(0, 0, true).AppendSegment(1, 0, true).AppendSegment(1, 1, true).AppendSegment(0, 1, true)
                .AppendSegment(0, 0, true);

            box.AddFill("1", 0, 0, 1, 0).AddFill("1", 1, 0, 1, 1).AddFill("1", 1, 1, 0, 1).AddFill("1", 0, 1, 0, 0);

            box.SaveToFile(fileName);
        }

        private static void SaveToFile(this Scheme scheme, string fileName) {
            using var file = File.CreateText(fileName);
            JsonSerializer.CreateDefault(new JsonSerializerSettings { Formatting = Formatting.Indented }).Serialize(file, scheme, typeof(Scheme));
        }
    }
}
