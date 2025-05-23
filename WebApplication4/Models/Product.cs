﻿using Pronia.Models;

namespace WebApplication4.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Photo> photo { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
