using EShop.Application.User;
using EShop.Domain.Models;
using EShop.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsers();
        // var userViewModels = users.ConvertAll(user => new UserListViewModel
        // {
        //     Id = user.Id,
        //     FirstName = user.FirstName,
        //     LastName = user.LastName,
        //     Email = user.Email,
        //     UserType = user.UserType
        // }).ToList();
        var userViewModels = new List<UserListViewModel>
        {
            new UserListViewModel
            {
                Id = Guid.NewGuid(),
                FirstName = "user.FirstName",
                LastName = "user.LastName",
                Email = "user.Email",
                UserType = new UserType
                {
                    Id = Guid.NewGuid(),
                    Type = "user.UserType.Name"
                }
            }
        };
        return View(userViewModels);
    }
}