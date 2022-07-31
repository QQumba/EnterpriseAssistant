using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }


        public abstract class WithId<T> : BaseEntity
        {
            [Column("id")]
            public T Id { get; set; }
            
            [Column("is_soft_deleted")]
            public bool IsSoftDeleted { get; set; }
        }
    }
}