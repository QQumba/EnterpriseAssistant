namespace EnterpriseAssistant.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsSoftDeleted { get; set; }

        public abstract class WithId<T> : BaseEntity
        {
            public T Id { get; set; }
        }
    }
}