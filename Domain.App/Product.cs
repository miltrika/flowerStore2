using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Product: Product<Guid>, IDomainEntityId
    {
        
    }

    public class Product<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public ICollection<Price>? Prices { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        
        [Required] 
        public int Stock { get; set; }
    }
}