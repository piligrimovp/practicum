using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class Order
    {
        [Key]
        public int id { get; private set; }

        [Required]
        public int СlientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Количество {0} может быть больше либо равно {1} и меньше {2}")]
        public int Count { get; set; }

        public DateTime Date { get; set; }

        public Order(int client, int service, int count, DateTime date)
        {
            this.СlientId = client;
            this.ServiceId = service;
            this.Count = count;
            this.Date = date;
        }

        public Order() {}

    }
}
