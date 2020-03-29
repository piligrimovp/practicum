using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    public class Client
    {
        public int Id { get; private set; }
        [Required]
        [StringLength(50, ErrorMessage = "Длина {0} должны быть меньше {1} символов.")]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Число {0} должно быть больше {1} и меньше {2}")]
        public int Age { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Client(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }
}
