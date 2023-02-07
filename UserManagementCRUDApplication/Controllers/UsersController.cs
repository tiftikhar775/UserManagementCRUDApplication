using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementCRUDApplication.Data;
using UserManagementCRUDApplication.Models;
using UserManagementCRUDApplication.Models.DomainModels;

namespace UserManagementCRUDApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVCDbContext context;

        // inject db service in controller constructor
        public UsersController(MVCDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        // index method, shows list of created users
        public async Task<IActionResult> Index()
        {
            var users = await context.Users.ToListAsync();
            return View(users);
        }

        [HttpGet] // init as get method
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] // init as post method
        public async Task<IActionResult> Add(AddUserViewModel addUserRequest)
        {
            // init domain model, allows values to be stored in database
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = addUserRequest.UserName,
                RealName = addUserRequest.RealName,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
                DateOfBirth = addUserRequest.DateOfBirth
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null) 
            {
                var viewModel = new UpdateUserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    Email = user.Email,
                    Password = user.Password,
                    DateOfBirth = user.DateOfBirth
                };

                return await Task.Run(() => View("View", viewModel)); 
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        // update user fields method
        public async Task<IActionResult> View(UpdateUserViewModel update)
        {
            var user = await context.Users.FindAsync(update.Id);
            if (user != null)
            {
                user.UserName = update.UserName;
                user.RealName = update.RealName;
                user.Email = update.Email;  
                user.Password = update.Password;    
                user.DateOfBirth = update.DateOfBirth;

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");    
        }

        [HttpPost] 
        // delete user method
        public async Task<IActionResult> Delete(UpdateUserViewModel update)
        {
            var user = await context.Users.FindAsync(update.Id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();   

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
