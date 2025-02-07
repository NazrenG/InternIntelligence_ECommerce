﻿namespace ECommerce.WebAPI.Dtos
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string? CategoryName { get; set; } 
    }
}
