using ApiRestRoulette.Context;
using ApiRestRoulette.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestRoulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : Controller
    {
        private readonly AppDbContext _context;

        public RouletteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Roulette
        [HttpGet]
        public  ActionResult SpinRoulette()
        {
            var random = new Random();
            int number = random.Next(0, 37);
            string color;
            int colorNumber = random.Next(0, 2);
            if(colorNumber == 0)
            {
                color = "red";
            }
            else
            {
                color = "black";
            }
            return Ok(new {Number = number, Color = color});
        }

        // POST: api/Roulette/prize
        [HttpPost("prize")]
        public ActionResult PostPrize(Bet bet) {
            try
            {
                decimal prize = 0;
                BetType typeBet = bet.TypeBet;

                switch (typeBet)
                {
                    case BetType.NumberColor:
                        prize = 3 * bet.BetMoney;
                        break;
                    case BetType.EvenOdd:
                        prize = 2 * bet.BetMoney;
                        break;
                    case BetType.Color:
                        prize = bet.BetMoney / 2;
                        break;
                    default:
                        prize = 0;
                        break;
                }

                return Ok(new { Prize = prize });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
