using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceiptProcessorChallenge_CSharp.Entities;
using ReceiptProcessorChallenge_CSharp.Entities.Rules;
using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Controllers
{
    [ApiController]
    [Route("receipts")]
    [Produces("application/json")]
    public class ReceiptsController : Controller
    {
        private readonly PointContext _context;
        private readonly List<IRule> pointRules;

        public ReceiptsController(PointContext context)
        {
            _context = context;
            pointRules =
            [
                new AlphanumericRetailerNameRule(),
                new EveryTwoItemsRule(),
                new ItemDescriptionLengthRule(),
                new PurchaseDateRule(),
                new PurchaseTimeRule(),
                new TotalIsMultipleRule(),
                new TotalWholeDollarRule(),
                new ItemStartsWithLetter(),
            ];
        }

        // POST: receipts/process
        [HttpPost("process")]
        [ProducesResponseType(typeof(Point), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Process(Receipt receipt)
        {
            if(!ReceiptCustomValidation.IsValid(receipt))
            {
                return StatusCode(400, "The receipt is invalid");
            }

            string id = Guid.NewGuid().ToString();
            Point point = new();
            point.Id = id;
            point.Points = CalculatePoints(receipt);
            _context.Points.Add(point);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                if(_context.Points.Any(e => e.Id == id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest($"Something went wrong saving to in memory database {ex.Message}");
                }
            }
            return Ok(new {id});
        }

        // GET: receipts/{id}/points
        [HttpGet("{id}/points")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Points(string id)
        {
            Point? points = await _context.Points.FindAsync(id);

            if(points == null)
            {
                return NotFound("No receipt found for that id");
            }

            return Ok(new { points = points.Points });
        }

        // DELETE: receipts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePoints(string id)
        {
            Point? points = await _context.Points.FindAsync(id);

            if(points == null)
            {
                return NotFound("No receipt found for that id");
            }

            _context.Points.Remove(points);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                return BadRequest($"Something went wrong saving to in memory database {ex.Message}");
            }

            return Ok(new { points });
        }

        private int CalculatePoints(Receipt receipt)
        {
            int result = 0;

            foreach(IRule rule in pointRules)
            {
                result += rule.CalculatePoints(receipt);
            }

            return result;
        }
    }
}
