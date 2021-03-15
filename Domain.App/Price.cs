using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Price: Price<Guid>, IDomainEntityId
    {
        
    }

    public class Price<TKey> : DomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey ProductId { get; set; } = default!;
        public Product? Product { get; set; }

        [Required] 
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Amount { get; set; }

        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}