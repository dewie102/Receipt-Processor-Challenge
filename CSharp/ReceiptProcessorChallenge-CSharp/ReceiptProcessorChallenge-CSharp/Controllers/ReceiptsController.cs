using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Controllers
{
    [ApiController]
    [Route("receipts")]
    [Produces("application/json")]
    public class ReceiptsController : Controller
    {
        private readonly ReceiptContext _context;

        public ReceiptsController(ReceiptContext context)
        {
            _context = context;
        }

        // POST: receipts/process
        [HttpPost("process")]
        public async Task<ActionResult> Process(Receipt receipt)
        {
            string id = Guid.NewGuid().ToString();
            receipt.Id = id;
            _context.Receipts.Add(receipt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if(_context.Receipts.Any(e => e.Id == id))
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
            Receipt? receipt = await _context.Receipts.FindAsync(id);

            if(receipt == null)
            {
                return NotFound("No receipt found for that id");
            }

            return Ok(new {points = CalculatePoints(receipt)});
        }

        private int CalculatePoints(Receipt receipt)
        {
            return (int)double.Parse(receipt.Total);
        }
    }
}
