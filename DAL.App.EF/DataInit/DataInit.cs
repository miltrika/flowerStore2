using System;
using System.Collections.Generic;
using System.Linq;
using DAL.App.EF.DataInit.Faker;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.DataInit
{
    public class DataInit
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();

        }

        public static bool DropDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }
        
        public static void SeedAppData(AppDbContext context)
        {
            var products = GenerateProductsListFromFaker(20);
            products.ForEach(ps => context.Products.Add(ps));

            var orderRows = CreateDomainOrderRows(products, 50);
            orderRows.ForEach(or => context.OrderRows.Add(or));

            context.SaveChanges();
        }

        private static List<Domain.App.Product> GenerateProductsListFromFaker(int count)
        {
            var products = new List<Domain.App.Product>();
            for (var i = 0; i < count; i++)
            {
                var fakeProduct = new ProductFaker().Generate();
            
                products.Add(new Product()
                {
                    Name = fakeProduct.Name,
                    Stock = fakeProduct.Stock,
                    Prices = GenerateListOfPriceFromFaker(1)
                });
            }
            return products;
        }
        
        private static List<Domain.App.Price> GenerateListOfPriceFromFaker(int count)
        {
            var fakerPrices = new PriceFaker(DateTime.UtcNow.Subtract(TimeSpan.FromHours(24)),
                DateTime.UtcNow.Add(TimeSpan.FromDays(24))).Generate(count);

            return fakerPrices.Select(fp => new Price()
            {
                Amount = fp.Amount,
                From = fp.From,
                To = fp.To
            }).ToList();
        }
        
        private static List<Domain.App.OrderRow> CreateDomainOrderRows(List<Product> products,
            int count)
        {
            var rand = new Random();
            var orderRows = new List<OrderRow>();
            for (var i = 0; i < count; i++)
            {
                var insertProducts = products.OrderBy(p => Guid.NewGuid())
                    .Take(rand.Next(1,4))
                    .ToList();
                var order = new OrderFaker().Generate();
                var tempOrderRows = new List<OrderRow>();
                
                foreach (var product in insertProducts)
                {
                    tempOrderRows.Add(CreateOrderRow(product, order, rand.Next(1, 10)));
                }
                AddOrderTotalPrice(order, tempOrderRows.Sum(x => x.SubTotal));
                orderRows.AddRange(tempOrderRows);
            }

            return orderRows;
        }

        private static OrderRow CreateOrderRow(Product product, Order order, int quantity)
        {
            var subTotal = decimal.Round(
                product.Prices!.First().Amount * quantity,
                2,
                MidpointRounding.AwayFromZero);

            return new OrderRow()
            {
                Product = product,
                Order = order,
                Quantity = quantity,
                SubTotal = subTotal
            };
        }
        private static Domain.App.Order AddOrderTotalPrice(Order order, decimal totalPrice)
        {
            var netTotal = decimal.Round(totalPrice / (decimal) 1.2, 2, MidpointRounding.AwayFromZero);

            order.NetTotal = netTotal;
            order.Total = totalPrice;
            order.Tax = totalPrice - netTotal;
            return order;
        }
    }
}