using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserControling.Data;
using UserControlling.Models;

namespace UserControlling.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public HomeController(UserManager<User> userManager, AppDbContext context, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            _context = context;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList(); // Retrieve the list of users from the database
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> BlockUsers(List<string> userIds)
        {
            if (userIds != null && userIds.Any())
            {
                var currentUserId = userManager.GetUserId(User);

                foreach (var userId in userIds)
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null && user.Id == currentUserId)
                    {
                        await signInManager.SignOutAsync();
                    }
                    if (user != null)
                    {
                        user.IsActive = false;
                        await userManager.UpdateAsync(user);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(List<string> userIds)
        {
            if (userIds != null && userIds.Any())
            {
                foreach (var userId in userIds)
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        user.IsActive = true;
                        await userManager.UpdateAsync(user); 
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUsers(List<string> userIds)
        {
            if (userIds != null && userIds.Any())
            {
                foreach (var userId in userIds)
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        await userManager.DeleteAsync(user);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}