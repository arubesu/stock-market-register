using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockMarketRegister.API.Entities;
using StockMarketRegister.API.Models;
using StockMarketRegister.API.Services;
using System;
using System.Collections.Generic;

namespace StockMarketRegister.API.Controllers
{
    [Route("api/clients")]
    public class ClientController : Controller
    {
        private readonly IStockMarketRepository _repository;

        public ClientController(IStockMarketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var clientsFromRepo = _repository.GetClients();

            var clients = Mapper.Map<IEnumerable<ClientDto>>(clientsFromRepo);
            return Ok(clients);
        }

        [HttpGet("{id}", Name = "GetClient")]
        public IActionResult GetClient(Guid id)
        {
            var clientsFromRepo = _repository.GetClient(id);

            if (clientsFromRepo == null)
            {
                return NotFound();
            }

            var client = Mapper.Map<ClientDto>(clientsFromRepo);

            return Ok(client);
        }

        [HttpPost()]
        public IActionResult CreateClient([FromBody] ClientForCreationDto clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest();
            }

            var entity = Mapper.Map<Client>(clientDto);

            _repository.AddClient(entity);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            var clientToReturn = Mapper.Map<ClientDto>(entity);

            return CreatedAtRoute("GetClient", new { id = clientToReturn.Id }, clientToReturn);
        }

        [HttpPut("{clientId}")]
        public IActionResult UpdateClient(Guid clientId, [FromBody] ClientForUpdateDto clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest();
            }

            var clientFromRepo = _repository.GetClient(clientId);

            if (clientFromRepo == null)
            {
                return NotFound();
            }

            Mapper.Map(clientDto, clientFromRepo);

            _repository.UpdateClient(clientFromRepo);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            return NoContent();
        }


        [HttpDelete("{clientId}")]
        public IActionResult DeleteClient(Guid clientId)
        {
            var clientFromRepo = _repository.GetClient(clientId);

            if (clientFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteClient(clientFromRepo);

            if (!_repository.Save())
            {
                return StatusCode(500);
            }

            return NoContent();
        }

    }
}
