using System;
using System.Collections.Generic;
using System.Linq;
using StockMarketRegister.API.Entities;

namespace StockMarketRegister.API.Services
{
    public class StockMarketRepository : IStockMarketRepository
    {
        private StockMarketContext _context;

        public StockMarketRepository(StockMarketContext context)
        {
            _context = context;
        }

        public void AddClient(Client client)
        {
            client.Id = new Guid();
            _context.Clients.Add(client);
        }

        public void AddOrderForClient(Guid clientId, Order order)
        {
            var client = GetClient(clientId);

            if(client != null)
            {
                if(order.Id == Guid.Empty)
                {
                    order.Id = new Guid();
                }

                order.Client = client;
                order.ClientId = clientId;
            }
        }

        public void AddStock(Stock stock)
        {
            stock.Id = new Guid();
            _context.Stocks.Add(stock);
        }

        public void DeleteClient(Client client)
        {
            _context.Clients.Remove(client);
        }

        public void DeleteStock(Stock stock)
        {
            _context.Stocks.Remove(stock);
        }

        public Client GetClient(Guid clientId)
        {
           return _context.Clients.FirstOrDefault(c => c.Id == clientId);
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.OrderBy(c => c.Name);
        }

        public Order GetOrderForClient(Guid clientId, Guid orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId && o.ClientId == clientId).FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdersForClient(Guid clientId)
        {
            return _context.Orders.Where(o => o.ClientId == clientId).ToList();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }


        public Stock GetStock(Guid stockId)
        {
            return _context.Stocks.FirstOrDefault(s => s.Id == stockId);
        }

        public IEnumerable<Stock> GetStocks()
        {
            return _context.Stocks.ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0; 
        }

        public void UpdateClient(Client client)
        {
        }

        public void UpdateStock(Stock stock)
        {
        }
    }
}
