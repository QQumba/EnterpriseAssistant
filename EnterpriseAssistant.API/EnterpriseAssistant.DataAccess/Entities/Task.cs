﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseAssistant.DataAccess.Entities
{
    public class Task : BaseEntity.WithId
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }
        
        public User User { get; set; }
    }
}