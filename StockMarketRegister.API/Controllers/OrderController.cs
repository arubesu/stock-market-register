using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockMarketRegister.API.Entities;
using StockMarketRegister.API.Models;
using StockMarketRegister.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarketRegister.API.Controllers
{
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly IStockMarketRepository _repository;

        public OrderController(IStockMarketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var ordersFromRepo = _repository.GetOrders();

            var orders = Mapper.Map<IEnumerable<OrderDto>>(ordersFromRepo);
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrder(Guid id)
        {
            var ordersFromRepo = _repository.GetOrder(id);

            if (ordersFromRepo == null)
            {
                return NotFound();
            }

            var order = Mapper.Map<OrderDto>(ordersFromRepo);

            return Ok(order);
        }

        [HttpPost()]
        public IActionResult CreateOrder([FromBody] OrderForCreationDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            var stockCode = orderDto.StockCode;

            if (!_repository.StockExists(stockCode, orderDto.OrderDate))
            {
                return StatusCode(412);
            }

            var entity = Mapper.Map<Order>(orderDto);

            var orderValue = CalculateOrderValue(orderDto.StockAmount, GetStockValueByStockCode(stockCode));

            entity.OrderValue = orderValue;
            entity.BrokerageFee = CalculateBrokerageFee(orderDto.Type, orderValue);
            entity.IncomeTax = CalculateIncomeTax(orderDto);

            _repository.AddOrderForClient(orderDto.ClientId, entity);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            var orderToReturn = Mapper.Map<OrderDto>(entity);

            return CreatedAtRoute("GetOrder", new { id = orderToReturn.Id }, orderToReturn);
        }

        private decimal GetStockValueByStockCode(string stockCode)
        {
            return _repository.GetStocks().FirstOrDefault(s => s.Code == stockCode).Price;
        }


        private decimal CalculateOrderValue(int stockAmount, decimal stockValue)
        {
            return stockAmount * stockValue;
        }

        private decimal CalculateBrokerageFee(char orderType, decimal orderValue)
        {

            if (orderType.Equals('C'))
                return orderValue * 0.0075m;

            if (orderType.Equals('V'))
                return orderValue * 0.0095m;

            throw new Exception("Invalid Operation");

        }

        private decimal CalculateIncomeTax(OrderForCreationDto order)
        {
            decimal income = 0;
            var orderType = order.Type;
            var stockCode = order.StockCode;

            if (orderType.Equals('C'))
                return 0;

            if (orderType.Equals('V'))
            {
                if (stockCode != null)
                {
                    var stockFromOrderDate = _repository.GetStocks().FirstOrDefault(s => s.Code == stockCode && s.Date.Date == order.OrderDate.Date);
                    var stockFromBuyDate = _repository.GetStocks().FirstOrDefault(s => s.Code == stockCode && s.Date.Date == order.BuyDate?.Date);

                    if (stockFromOrderDate != null && stockFromBuyDate != null)
                    {
                        decimal orderDateQuote = stockFromOrderDate.Price;
                        decimal buyDateQuote = stockFromBuyDate.Price;
                        decimal variation = orderDateQuote - buyDateQuote;
                        if (variation > 0)
                            income = (order.StockAmount * variation) * 0.15m;
                    }
                }
            }
            return income;
        }
    }
}

