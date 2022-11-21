using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAndal.API.Data;
using TestAndal.API.Models;

namespace TestAndal.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]

   
    public class TitleController: Controller
    {
        private readonly AndalDbContext andalDbContext;

        public TitleController(AndalDbContext andalDbContext)
        {
            this.andalDbContext = andalDbContext;
        }

        //Get All Title
        [HttpGet]
        public async Task<IActionResult> GetAllTitle()
        {
            var titles = await andalDbContext.Titles.ToListAsync();
            return Ok(titles);
        }

        //Get Sigle Title

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetTitle")]
        public async Task<IActionResult> GetTitle([FromRoute] int id)
        {
            var title = await andalDbContext.Titles.FirstOrDefaultAsync(x => x.Id == id);
            if (title != null)
            {
                return Ok(title);
            }
            return NotFound("Title not Foud");
        }

        //add Title
        [HttpPost]
        public async Task<IActionResult> AddTitle([FromBody] CreateTitleDto request)
        {
            var newTitle = new Title
            {
                TitleCode= request.TitleCode,
                TitleName= request.TitleName,

            };
            //title.ID = Guid.NewGuid();
            await andalDbContext.Titles.AddAsync(newTitle);
            await andalDbContext.SaveChangesAsync();

            return Ok(newTitle);
        }

        //update Title
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTitle([FromRoute] int id, CreateTitleDto request)
        {
            var existingTitle = await andalDbContext.Titles.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTitle != null)
            {
                existingTitle.TitleCode = request.TitleCode;
                existingTitle.TitleName = request.TitleName;
                await andalDbContext.SaveChangesAsync();
                return Ok(existingTitle);
            }
            return NotFound("Title not found");
        }

        //Delete Title
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteTitle([FromRoute] int id)
        {
            var existingTitle = await andalDbContext.Titles.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTitle != null)
            {
                andalDbContext.Remove(existingTitle);
                await andalDbContext.SaveChangesAsync();
                return Ok(existingTitle);
            }
            return NotFound("Title not found");
        }
    }
}
