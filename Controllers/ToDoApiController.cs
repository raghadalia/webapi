using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.interfaces;
using ToDoApi.Models;
using System.Threading.Tasks;
using ToDoApi.Data;
using ToDoApi.Interfaces;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/ToDoList")]
    public class ToDoApiController : ControllerBase
    {
        private readonly IToDosRepository _toDosRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;


        public ToDoApiController(IToDosRepository toDosRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _toDosRepository = toDosRepository;
            _userManager = userManager;
            _context = context;
        }

        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var userTasks = await _toDosRepository.GetAllAsync(currentUser.Id);
            return Ok(userTasks);
        }

        [Authorize]
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var toDos = await _toDosRepository.GetByIdAsync(id);
            if (toDos == null)
            {
                return NotFound();
            }

            return Ok(toDos);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ToDos toDos)
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized();
            }

            toDos.User = currentUser;
            await _toDosRepository.CreateAsync(toDos);
            return CreatedAtAction("GetToDosById", new { id = toDos.Id }, toDos);
        }

        [Authorize  ]
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody] ToDos toDos)
        {
            if (id != toDos.Id)
            {
                return BadRequest();
            }

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized();
            }

            toDos.User = currentUser;
            var TODOItem = await _context.ToDos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (TODOItem == null)
            {
                return NotFound();
            }
            TODOItem.Title = toDos.Title;
            TODOItem.Description = toDos.Description;
            TODOItem.User = toDos.User;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();

        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized();
            }

            await _toDosRepository.DeleteAsync(id);
            return NoContent();
     }
        private bool ToDosExists(int id)
        {
            return (_context.ToDos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
