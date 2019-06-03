using CustomerInquiry.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerInquiry.Tests
{
    public abstract class CustomerInquiryTestBase : IDisposable
    {
        protected readonly CustomerInquiryContext DbContext;

        protected CustomerInquiryTestBase()
        {
            var options = new DbContextOptionsBuilder<CustomerInquiryContext>()
                .UseInMemoryDatabase(databaseName: "CustomerInquiry")
                .Options;

            DbContext = new CustomerInquiryContext(options);

            SeedContext(DbContext);
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }

        private void SeedContext(CustomerInquiryContext dbContext)
        {

            if (dbContext.Customers.Any() && dbContext.Transactions.Any())
            {
                // Skip seed data
                return;
            }

            var customer = new Customer
            {
                CustomerID = 1,
                Email = "test@test.com",
                Mobile = "0987654321",
                Name = "fullname lastname"
            };

            dbContext.Customers.Add(customer);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = 1,
                    CustomerID = 1,
                    Amount = 1500.00m,
                    Currency = "THB",
                    Date = DateTime.UtcNow,
                    Status = "Success"
                },
                new Transaction
                {
                    Id = 2,
                    CustomerID = 1,
                    Amount = 59.99m,
                    Currency = "USD",
                    Date = DateTime.UtcNow,
                    Status = "Failed"
                }
            };

            dbContext.Transactions.AddRange(transactions);
            dbContext.SaveChanges();
        }
    }
}
