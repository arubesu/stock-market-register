using StockMarketRegister.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketRegister.API.Models
{
    public class OrderForCreationDto
    {
        public char Type { get; set; }
        public Guid ClientId { get; set; }
        public string StockCode { get; set; }
        public int StockAmount { get; set; }
        public DateTime? BuyDate { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
