using StockMarketRegister.API.Entities;
using System;
using System.Collections.Generic;

namespace StockMarketRegister.API.Services
{
    public interface IStockMarketRepository
    {
        void AddClient(Client client);
        IEnumerable<Client> GetClients();
        Client GetClient(Guid clientId);
        void UpdateClient(Client client);
        void DeleteClient(Client client);

        void AddStock(Stock stock);
        IEnumerable<Stock> GetStocks();
        Stock GetStock(Guid stockId);
        void UpdateStock(Stock stock);
        void DeleteStock(Stock stock);
        bool StockExists(string stockCode, DateTime orderDate);

        IEnumerable<Order> GetOrdersForClient(Guid clientId);
        IEnumerable<Order> GetOrders();
        Order GetOrder(Guid orderId);
        void AddOrderForClient(Guid clientId, Order order);

        bool Save();
    }
}
