using System;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Price: Price<Guid>, IDomainEntityId
    {
        
    }

    public class Price<TKey> : DomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        public TKey ProductId { get; set; } = default!;
        public Product? Product { get; set; }
        
        public decimal Amount { get; set; }

        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}