﻿namespace Domain.Common
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}
