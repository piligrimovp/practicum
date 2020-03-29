using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class Service
    {
        public int Id { get; private set; }
        [Required]
        [StringLength(50, ErrorMessage = "Длина {0} должны быть меньше {1} символов.")]
        public string Name { get; set; }
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Число {0} должно быть больше {1} и меньше {2}")]
        public double Price { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Service(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
