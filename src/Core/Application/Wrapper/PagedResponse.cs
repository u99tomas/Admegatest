﻿namespace Application.Wrapper
{
    public class PagedResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
