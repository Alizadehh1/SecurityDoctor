﻿using System;

namespace SecurityDoctor.WebUI.AppCode.Infrastructure
{
    public class BaseEntity : HistoryEntity
    {
        public int Id { get; set; }
    }
    public class HistoryEntity
    {
        public int? CreatedById { get; set; }
        public DateTime CreatedDated { get; set; } = DateTime.UtcNow.AddHours(4);
        public int? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
