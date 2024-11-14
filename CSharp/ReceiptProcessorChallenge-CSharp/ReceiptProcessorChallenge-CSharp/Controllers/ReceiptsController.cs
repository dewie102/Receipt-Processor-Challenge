using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //private readonly ReceiptContext _context;
        private readonly List<IRule> pointRules;

        public ReceiptsController(PointContext context)
        //public ReceiptsController(ReceiptContext context)
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
            ];
        }

        // POST: receipts/process
        [HttpPost("process")]
        public async Task<ActionResult> Process(Receipt receipt)
        {
            string id = Guid.NewGuid().ToString();
            Point point = new Point();
            point.Id = id;
            point.Points = CalculatePoints(receipt);
            _context.Points.Add(point);
            /*receipt.Id = id;
            _context.Receipts.Add(receipt);*/
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if(_context.Points.Any(e => e.Id == id))
                //if(_context.Receipts.Any(e => e.Id == id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest("The receipt is invalid");
                }
            }
            return Ok(new {id});
        }

        // GET: receipts/{id}/points
        [HttpGet("{id}/points")]
        public async Task<ActionResult> Points(string id)
        {
            Point? points = await _context.Points.FindAsync(id);

            if(points == null)
            {
                return NotFound("No receipt found for that id");
            }

            return Ok(new { points = points.Points });

            /*Receipt? receipt = await _context.Receipts.FindAsync(id);

            if(receipt == null)
            {
                return NotFound("No receipt found for that id");
            }

            return Ok(new { points = CalculatePoints(receipt) });*/
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
