using Bogus;
using Domain.App;

namespace DAL.App.EF.DataInit.Faker
{
    public sealed class ProductFaker: Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Name, f => f.Name.FirstName());
            RuleFor(p => p.Stock, f => f.Random.Number(100));
        }
    }
}