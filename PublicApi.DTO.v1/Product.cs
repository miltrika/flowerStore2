using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Product : ProductCreate
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductCreate
    {
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        [Required]
        [Range(0, 100000000)]
        public int Stock { get; set; }
    }
}