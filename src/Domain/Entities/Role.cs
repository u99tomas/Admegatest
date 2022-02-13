﻿using Domain.Common;

namespace Domain.Entities
{
    public class Role : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
