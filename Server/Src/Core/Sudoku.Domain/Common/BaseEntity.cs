using System;

namespace Sudoku.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
        public static Guid GenerateId()
        {
            return Guid.NewGuid();
        }

    }
    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }
}
