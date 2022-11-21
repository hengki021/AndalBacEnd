using Microsoft.AspNetCore.Mvc;
using TestAndal.API.Data;
using TestAndal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TestAndal.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]

    public class PositionController: Controller
    {
        private readonly AndalDbContext andalDbContext;

        public PositionController(AndalDbContext andalDbContext)
        {
            this.andalDbContext = andalDbContext;
        }


        //Get All Position
        [HttpGet]
        public async Task<IActionResult> GetAllPosition()
        {

            var GetPosition = (from p in andalDbContext.Positions
                               join t in andalDbContext.Titles
                               on p.TitleId equals t.Id


                               select new TitlePositionDto
                               {
                                   Id = p.Id,
                                   PositionCode = p.PositionCode,
                                   PositionName = p.PositionName,
                                   TitleName = t.TitleName
                               }).ToList();

            return Ok(GetPosition);
        }

        //Get Sigle Title

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetPosition")]
        public async Task<IActionResult> GetPosition([FromRoute] int id)
        {
            var position = await andalDbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (position != null)
            {
                return Ok(position);
            }
            return NotFound("Position not Foud");
        }

        //add Position
        [HttpPost]
        public async Task<IActionResult> AddPosition([FromBody] CreatePositionDto request)
        {
            var newPosition = new Position
            {
                PositionCode = request.PositionCode,
                PositionName = request.PositionName,
                TitleId = request.TitleId,

            };
            await andalDbContext.Positions.AddAsync(newPosition);
            await andalDbContext.SaveChangesAsync();

            return Ok(newPosition);
        }

        //update Position
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePosition([FromRoute] int id, CreatePositionDto request)
        {
            var existingPosition = await andalDbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingPosition != null)
            {
                existingPosition.PositionCode = request.PositionCode;
                existingPosition.PositionName = request.PositionName;
                existingPosition.TitleId = request.TitleId;
                await andalDbContext.SaveChangesAsync();
                return Ok(existingPosition);
            }
            return NotFound("Position not found");
        }

        //Delete Position
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePosition([FromRoute] int id)
        {
            var existingPosition = await andalDbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingPosition != null)
            {
                andalDbContext.Remove(existingPosition);
                await andalDbContext.SaveChangesAsync();
                return Ok(existingPosition);
            }
            return NotFound("Position not found");
        }
    }

}
