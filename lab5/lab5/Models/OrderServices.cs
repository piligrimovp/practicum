using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
    [Table("OrderServices")]
    public class OrderServices
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Количество {0} может быть больше либо равно {1} и меньше {2}")]
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}
