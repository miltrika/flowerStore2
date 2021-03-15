using System;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class OrderRow: OrderRow<Guid>, IDomainEntityId
    {
        
    }

    public class OrderRow<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ProductId { get; set; } = default!;
        public Product? Product { get; set; }
        
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal SubTotal { get; set; }
    }
}