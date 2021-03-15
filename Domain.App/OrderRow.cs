using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class OrderRow: OrderRow<Guid>, IDomainEntityId
    {
        
    }

    public class OrderRow<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        [Required]
        public TKey ProductId { get; set; } = default!;
        public Product? Product { get; set; }
        
        [Required]
        public TKey OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(9, 2)")]
        public decimal SubTotal { get; set; }
    }
}