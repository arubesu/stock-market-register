using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketRegister.API.Entities
{
    public static class StockMarketContextExtensions
    {
        public static void EnsureSeedDataForContext(this StockMarketContext context)
        {

            var clients = new List<Client>()
            {
                new Client()
                {
                    Id = new Guid(),
                    Name = "Renan Sousa Alves",
                    BirthDate = new DateTime(1990,12,31),
                    CpfCnpj = "93716015580",
                    PersonType = 'F'
                },
                new Client()
                {
                    Id = new Guid(),
                    Name = "Julieta Santos Araujo",
                    BirthDate = new DateTime(1990,10,31),
                    CpfCnpj = "39378142303",
                    PersonType = 'F'
                },
                new Client()
                {
                    Id = new Guid(),
                    Name = "Kai Barros Gomes",
                    BirthDate = new DateTime(1980,10,10),
                    CpfCnpj = "71182217320",
                    PersonType = 'F'
                },
                new Client()
                {
                    Id = new Guid(),
                    Name = "Luiz Mathias",
                    BirthDate = new DateTime(1980,05,10),
                    CpfCnpj = "52658885000114",
                    PersonType = 'J'
                },
                new Client()
                {
                    Id = new Guid(),
                    Name = "Luiz Mathias",
                    BirthDate = new DateTime(1980,01,03),
                    CpfCnpj = "33358477000100",
                    PersonType = 'J'
                }
            };

            var stocks = new List<Stock>()
            {
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,28),
                    Price = 23.55m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,27),
                    Price = 23.68m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,26),
                    Price = 23.67m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,25),
                    Price = 23.18m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,24),
                    Price = 23.55m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,23),
                    Price = 23.20m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,22),
                    Price = 23.30m
                },
                new Stock()
                {
                    Id = new Guid(),
                    Code = "BPAC11",
                    Date = new DateTime(2018,12,21),
                    Price = 23.76m
                }
            };

            context.Clients.AddRange(clients);
            context.Stocks.AddRange(stocks);
            context.SaveChanges();

        }

    }
}
