using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardType { get; set; }
        public int? IitemAmoint { get; set; }

        public int orderNumber { get; set; }
        [ForeignKey("orderNumber")]
        public Order Order { get; set; }
    }
}
