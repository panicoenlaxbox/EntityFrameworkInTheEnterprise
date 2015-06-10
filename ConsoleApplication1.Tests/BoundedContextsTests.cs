using System;
using System.Linq;
using NUnit.Framework;

namespace ConsoleApplication1.Tests
{
    public class BoundedContextsTests
    {
        [Test]
        public void CanInsertCustomer()
        {
            using (var context = new PrimaryContext())
            {
                context.Database.Initialize(false);
                var customer = new Customer
                {
                    Name = "Customer 1",
                    Code = "C1"
                };
                context.Customers.Add(customer);
                context.SaveChanges();
                Assert.Pass();
            }
        }

        [Test]
        public void CanRetrieveCustomerReference()
        {
            using (var context = new SecondaryContext())
            {
                var customerReference = context.Customers.SingleOrDefault(p => p.Name == "Customer 1");
                Assert.IsNotNull(customerReference);
            }
        }
    }
}
