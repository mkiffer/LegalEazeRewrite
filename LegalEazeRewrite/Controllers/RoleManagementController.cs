using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LegalEazeRewrite.Models.DataModels;
using System.Linq;
using System.Threading.Tasks;
using LegalEazeRewrite.Models.ViewModels;

namespace LegalEazeRewrite.Controllers;


[Authorize(Roles = "admin")]
public class RoleManagementController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleManagementController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var thisViewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            userRolesViewModel.Add(thisViewModel);
        }

        return View(userRolesViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Manage(string userId)
    {
        ViewBag.userId = userId;
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var model = new ManageUserRolesViewModel
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = _roleManager.Roles.Select(r => new RoleSelectionViewModel
            {
                RoleName = r.Name,
                Selected = _userManager.IsInRoleAsync(user, r.Name).Result
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(ManageUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var selectedRoles = model.Roles.Where(x => x.Selected).Select(y => y.RoleName).ToList();
        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToList());

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to add roles");
            return View(model);
        }

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToList());

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to remove roles");
            return View(model);
        }

        return RedirectToAction("Index");
    }
}

