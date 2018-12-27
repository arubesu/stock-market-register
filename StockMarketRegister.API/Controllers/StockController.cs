using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockMarketRegister.API.Entities;
using StockMarketRegister.API.Models;
using StockMarketRegister.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketRegister.API.Controllers
{
    [Route("api/stocks")]
    public class StockController : Controller
    {
        private readonly IStockMarketRepository _repository;

        public StockController(IStockMarketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocksFromRepo = _repository.GetStocks();

            var stocks = Mapper.Map<IEnumerable<StockDto>>(stocksFromRepo);
            return Ok(stocks);
        }

        [HttpGet("{id}", Name = "GetStock")]
        public IActionResult GetStock(Guid id)
        {
            var stocksFromRepo = _repository.GetStock(id);

            if (stocksFromRepo == null)
            {
                return NotFound();
            }

            var stock = Mapper.Map<StockDto>(stocksFromRepo);

            return Ok(stock);
        }

        [HttpPost()]
        public IActionResult CreateStock([FromBody] StockForCreationDto stockDto)
        {
            if (stockDto == null)
            {
                return BadRequest();
            }

            var entity = Mapper.Map<Stock>(stockDto);
            entity.Date = DateTime.Now;

            _repository.AddStock(entity);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            var stockToReturn = Mapper.Map<StockDto>(entity);

            return CreatedAtRoute("GetStock", new { id = stockToReturn.Id }, stockToReturn);
        }

        [HttpPut("{stockId}")]
        public IActionResult UpdateStock(Guid stockId, [FromBody] StockForUpdateDto stockDto)
        {
            if (stockDto == null)
            {
                return BadRequest();
            }

            var stockFromRepo = _repository.GetStock(stockId);
            stockFromRepo.Date = DateTime.Now;

            if (stockFromRepo == null)
            {
                return NotFound();
            }

            Mapper.Map(stockDto, stockFromRepo);

            _repository.UpdateStock(stockFromRepo);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            return NoContent();
        }


        [HttpDelete("{stockId}")]
        public IActionResult DeleteStock(Guid stockId)
        {
            var stockFromRepo = _repository.GetStock(stockId);

            if (stockFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteStock(stockFromRepo);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
