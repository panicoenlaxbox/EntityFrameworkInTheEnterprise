using System.Data.Entity;

namespace ConsoleApplication1
{
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
}