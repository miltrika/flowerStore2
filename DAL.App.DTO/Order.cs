using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Order : Order<Guid>, IDomainEntityId
    {
    }

    public class Order<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public ICollection<OrderRow>? OrderRows { get; set; }

        public DateTime Time { get; set; }
        
        public decimal NetTotal { get; set; }
        
        public decimal Tax { get; set; }
        
        public decimal Total { get; set; }
    }
}