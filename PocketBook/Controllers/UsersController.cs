using Microsoft.AspNetCore.Mvc;
using PocketBook.Core.IConfiguration;
using PocketBook.Model;

namespace PocketBook.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController:ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitofWork _unitofWork;

        public UsersController(ILogger<UsersController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id=Guid.NewGuid();

                await _unitofWork.Users.Add(user);
                await _unitofWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { user.Id }, user);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var user=await _unitofWork.Users.GetById(id);
            if (user == null)
                return NotFound(); //404 Status Code
           
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users=await _unitofWork.Users.All();

            return Ok(users);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, User user)
        {
            if(id!=user.Id)
                return BadRequest();
            
            await _unitofWork.Users.Upsert(user);
            await _unitofWork.CompleteAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item=await _unitofWork.Users.GetById(id);
            
            if (item == null)
                return BadRequest();

            await _unitofWork.Users.Delete(id);
            await _unitofWork.CompleteAsync();

            return Ok(item);
        }
    }
}
