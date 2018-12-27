using System;

namespace StockMarketRegister.API.Models
{
    public class ClientForCreationDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char PersonType { get; set; }
        public string CpfCnpj { get; set; }
    }
}
