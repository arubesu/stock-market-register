using System;
using System.ComponentModel.DataAnnotations;

namespace StockMarketRegister.API.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public char Type { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public Client Client { get; set; }

        public Guid ClientId { get; set; }

        [Required]
        public string StockCode { get; set; }
        
        [Required]
        public int StockAmount { get; set; }

        public DateTime BuyDate { get; set; }

        public decimal OrderValue { get; set; }

        public decimal BrokerageFee { get; set; }

        public decimal IncomeTax { get; set; }
    }
}
