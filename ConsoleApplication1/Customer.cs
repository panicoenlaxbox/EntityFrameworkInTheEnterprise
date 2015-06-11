using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication1
{
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