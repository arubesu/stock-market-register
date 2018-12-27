using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketRegister.API.Models
{
    public class ClientForUpdateDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char PersonType { get; set; }
        public string CpfCnpj { get; set; }
    }
}
