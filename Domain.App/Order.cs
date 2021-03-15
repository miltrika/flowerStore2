using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Order : Order<Guid>, IDomainEntityId
    {
    }

    public class Order<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public ICollection<OrderRow>? OrderRows { get; set; }

        public DateTime Time { get; set; }
        
        [Column(TypeName = "decimal(13, 2)")]
        public decimal NetTotal { get; set; }
        
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Tax { get; set; }
        
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Total { get; set; }
    }
}