using System;
using System.ComponentModel.DataAnnotations;

namespace StockMarketRegister.API.Entities
{
    public class Client
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public char PersonType { get; set; }

        public string CpfCnpj { get; set; }
    }
}
