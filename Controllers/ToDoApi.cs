using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.interfaces;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoApi : ControllerBase
    {
        private readonly IToDosRepository _toDosRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoApi(IToDosRepository toDosRepository, UserManager<ApplicationUser> userManager)
        {
            _toDosRepository = toDosRepository;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        [Authorize(Roles = "User,Admin")]
        // GET: ToDos
        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserAsync();
            var userTasks = await _toDosRepository.GetAllAsync(currentUser.Id);
            return Ok(userTasks);
        }

        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> All()
        //{
        //    var toDos = await _toDosRepository.GetAllAsync();
        //    return View(toDos);
        //}

        [Authorize(Roles = "User")]
        // GET: ToDos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDos = await _toDosRepository.GetByIdAsync(id);
            if (toDos == null)
            {
                return NotFound();
            }

            return Ok(toDos);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsCompleted,DueDate,Categories,PriorityLevel")] ToDos toDos)
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser != null)
            {
                toDos.User = currentUser;
                await _toDosRepository.CreateAsync(toDos);
                return RedirectToAction(nameof(Index));
            }
            return Ok(toDos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsCompleted,DueDate,Categories,PriorityLevel")] ToDos toDos)
        {
            if (id != toDos.Id)
            {
                return NotFound();
            }

            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (currentUser != null)
                {
                    toDos.User = currentUser;
                    await _toDosRepository.UpdateAsync(toDos);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDosExists(toDos.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "User")]
        // GET: ToDos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDos = await _toDosRepository.GetByIdAsync(id);
            if (toDos == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _toDosRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ToDosExists(int id)
        {
            return _toDosRepository.Exists(id);
        }
    }
}