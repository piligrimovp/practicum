using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
 
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Длина {0} должны быть меньше {1} символов.")]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Число {0} должно быть больше {1} и меньше {2}")]
        public int Age { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
