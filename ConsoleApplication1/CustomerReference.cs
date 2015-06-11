using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApplication1
{
    // tanto CustomerReference como Customer se mapean a la misma tabla
    [Table("Customers")]
    public class CustomerReference
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}