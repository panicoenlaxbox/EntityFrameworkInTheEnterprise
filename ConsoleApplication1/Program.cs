using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

 
        }
    }

    // abstract no es necesario porque el constructor por defecto es protegido
    // el problema está en que el primer contexto instanciado se adueña de __MigrationHistory
    // si el primer contexto es Secondary, la tabla Customers no tendrá el campo Code
    //abstract class BaseContext : DbContext
    //{
    //    protected BaseContext()
    //        : base("name=ConsoleApplication1")
    //    {

    //    }
    //}

    //esto asume que trabajamos contra una base de datos existente
    public class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("name=ConsoleApplication1")
        {

        }
    }

    public class PrimaryContext : BaseContext<PrimaryContext>
    {
        public DbSet<Customer> Customers { get; set; }
    }

    public class SecondaryContext : BaseContext<PrimaryContext>
    {
        public DbSet<CustomerReference> Customers { get; set; }
    }

    // tanto CustomerReference como Customer se mapean a la misma tabla
    [Table("Customers")]
    public class CustomerReference
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // se mapea por defecto a tabla Customers
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
