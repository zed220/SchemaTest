using System.Collections.Generic;

namespace SchemeTester.Data {

    /// <summary>
    /// Класс-контейнер линий и заливок
    /// </summary>
    public sealed class Scheme {

        /// <summary>
        /// Линии. Линейный список, хотя это по сути дерево со связями <see cref="Segment.Id"/>-<see cref="Segment.ParentId"/>. Линейный список мне показался удобнее в программной реализации.
        /// </summary>
        public List<Segment> Segments { get; set; } = new();

        /// <summary>
        /// Словарь заливок. Ключ - название заливки. Значение - массив <see cref="Segment.Id"/>, которые формируют контур заливки. 
        /// </summary>
        public Dictionary<string, List<int>> Fills { get; set; } = new();
    }
}
