using System.Collections.Generic;

namespace SchemaTester.Data {
    /// <summary>
    /// Класс-контейнер линий и заливок
    /// </summary>
    public sealed class Schema {

        /// <summary>
        /// Линии. Линейный список, хотя это по сути дерево со связями <see cref="LineEnd.Id"/>-<see cref="LineEnd.ParentId"/>. Линейный список мне показался удобнее в программной реализации.
        /// </summary>
        public List<LineEnd> LineEnds { get; init; } = new();

        /// <summary>
        /// Словарь заливок. Ключ - название заливки. Значение - массив <see cref="LineEnd.Id"/>, которые формируют контур заливки. 
        /// </summary>
        public Dictionary<string, List<int>> Fills { get; init; } = new();
    }
}
