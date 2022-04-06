namespace SchemeTester.Data
{
    /// <summary>
    /// Класс-точка, к которой идёт линия.
    /// </summary>
    public sealed class Segment {

        /// <summary>
        /// Уникальный идентификатор сегмента. Нужен для того, чтобы на него могли ссылаться другие сегменты для построения линий.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор сегмента, от которого идёт этот сегмент. Если <see cref="ParentId"/> равен '-1', то это корень, к которому не ведёт ни одна линия.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Координата X в условных единицах
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Координата Y в условных единицах
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Указывает, рисовать ли точку для этого сегмента.
        /// </summary>
        public bool Highlighted { get; set; }
    }
}