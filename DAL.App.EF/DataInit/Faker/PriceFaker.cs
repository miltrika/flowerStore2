using System;
using Bogus;
using Domain.App;

namespace DAL.App.EF.DataInit.Faker
{
    public sealed class PriceFaker : Faker<Price>
    {
        public PriceFaker(DateTime from, DateTime to)
        {
            RuleFor(p => p.Amount, f =>
                decimal.Round(
                    f.Random.Decimal(0.1M, 3.5M),
                    2,
                    MidpointRounding.AwayFromZero));
            RuleFor(p => p.From, f => from);
            RuleFor(p => p.To, f => to);
        }
    }
}