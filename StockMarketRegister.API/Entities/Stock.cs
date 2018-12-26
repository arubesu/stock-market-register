using System;
using System.ComponentModel.DataAnnotations;

namespace StockMarketRegister.API.Entities
{
    public class Stock
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
