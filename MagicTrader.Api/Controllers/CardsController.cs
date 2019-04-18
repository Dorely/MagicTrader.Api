using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicTrader.Core.Context;
using MagicTrader.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IMagicCardContext _cardContext;
        public CardsController(IMagicCardContext cardContext)
        {
            _cardContext = cardContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById([FromRoute] Guid id)
        {
            var card = await _cardContext.GetCard(id);
            if(card == null)
            {
                return NoContent();
            }
            return Ok(card);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCardsByName([FromQuery] MagicCardQueryParams searchParams)
        {
            var cards = await _cardContext.GetCards(searchParams);
            if(cards == null || cards.Count == 0)
            {
                return NoContent();
            }
            return Ok(cards);
        }
    }
}
