using System.Data.Entity;

namespace ConsoleApplication1
{
    public class SecondaryContext : BaseContext<PrimaryContext>
    {
        public DbSet<CustomerReference> Customers { get; set; }
    }
}