using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Product: Product<Guid>, IDomainEntityId
    {
        
    }

    public class Product<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public ICollection<Price>? Prices { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }
        
        public string Name { get; set; } = default!;
        
        public int Stock { get; set; }
    }
}