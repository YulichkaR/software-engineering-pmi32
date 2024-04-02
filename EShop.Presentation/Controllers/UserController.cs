using EShop.Application.User;
using EShop.Domain.Models;
using EShop.Presentation.Models;
using EShop.Presentation.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public UserController(IUserService userService, UserManager<User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();

        var usersVm = users.ConvertAll(u => new UserListViewModel
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            UserType = u.UserType
        });
        return View(usersVm);
    }

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteUser(id);
        return RedirectToAction("Index");
    }
}