﻿namespace EnterpriseAssistant.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsSoftDeleted { get; set; }

        public abstract class WithId : BaseEntity
        {
            public long Id { get; set; }
        }
    }
}