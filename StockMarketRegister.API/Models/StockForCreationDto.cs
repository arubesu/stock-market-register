using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketRegister.API.Models
{
    public class StockForCreationDto
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
    }
}
