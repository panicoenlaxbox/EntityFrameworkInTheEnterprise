using System.Data.Entity;

namespace ConsoleApplication1
{
    public class PrimaryContext : BaseContext<PrimaryContext>
    {
        public DbSet<Customer> Customers { get; set; }
    }
}