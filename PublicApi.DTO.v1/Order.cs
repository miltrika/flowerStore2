using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class Order
    {
        public Guid Id { get; set; } = default!;
        public DateTime Time { get; set; }
        public ICollection<OrderRow>? OrderRows { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}