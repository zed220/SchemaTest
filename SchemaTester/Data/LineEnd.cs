using System.Drawing;

namespace SchemaTester.Data
{
    /// <summary>
    /// Класс-точка, к которой идёт линия.
    /// </summary>
    public sealed class LineEnd {
        public const int DefaultParentId = -1;
        
        /// <summary>
        /// Уникальный идентификатор. Нужен для того, чтобы на него могли ссылаться другие точки для построения линий.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Идентификатор, от которого идёт эта линия. Если <see cref="ParentId"/> равен <see cref="DefaultParentId"/>, то это корень, к которому не ведёт ни одна линия.
        /// </summary>
        public int ParentId { get; init; } = DefaultParentId;

        /// <summary>
        /// Точка, к которой ведёт данная линия. Если <see cref="ParentId"/> равен '-1', то это просто точка.
        /// </summary>
        public PointF Point { get; set; }

        /// <summary>
        /// Указывает, рисовать ли точку.
        /// </summary>
        public bool Highlighted { get; init; }
    }
}