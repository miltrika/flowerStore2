using System;
using Bogus;
using Domain.App;

namespace DAL.App.EF.DataInit.Faker
{
    public sealed class OrderFaker: Faker<Order>
    {
        public OrderFaker()
        {
            RuleFor(o => o.Time, f => f.Date.Between(
                DateTime.UtcNow.AddMinutes(10), DateTime.UtcNow.AddMinutes(50)
            ));
        }
    }
}