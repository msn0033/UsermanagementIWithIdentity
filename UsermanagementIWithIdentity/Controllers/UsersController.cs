using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsermanagementIWithIdentity.Models;
using UsermanagementIWithIdentity.ViewModel;

namespace UsermanagementIWithIdentity.Controllers
{
    [Authorize(Roles="admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult()
            }).ToListAsync();
            return View(users);
        } //end Index


        public async Task<IActionResult> ManageRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) { return NotFound(); }

            var roles = await _roleManager.Roles.ToListAsync();

            if (roles == null) { ModelState.AddModelError("role", "لايوجد رول  "); }

            var viewModel = new UserRoleViewModel
            {
                userId = user.Id,
                userName = user.UserName,
                Roles = roles.Select(role => new RolesViewModel
                {
                    roleId = role.Id,
                    roleName = role.Name,
                    IsSelect = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> ManageRole(UserRoleViewModel userRoleViewModel)
        {
            if (userRoleViewModel == null) { return NotFound(); }

            var user = await _userManager.FindByIdAsync(userRoleViewModel.userId);
            if (user == null) { return NotFound(); }

            var userRole = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoleViewModel.Roles)
            {
                if (userRole.Any(r => r == role.roleName) && !role.IsSelect)
                    await _userManager.RemoveFromRoleAsync(user, role.roleName);

                if (!userRole.Any(r => r == role.roleName) && role.IsSelect)
                    await _userManager.AddToRoleAsync(user, role.roleName);

            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.Select(r => new RolesViewModel { roleId = r.Id, roleName = r.Name, IsSelect = false }).ToListAsync();

            var viewModel = new AddUserViewModel
            {
                roles = roles
            };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (!model.roles.Any(x => x.IsSelect))
            {
                ModelState.AddModelError("roles", "رجاء اختيار رول واحد اقل شي");
                return View(model);
            }


            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "الايميل موجود سابقاً");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "الاسم موجود سابقاً");
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("roles", item.Description.ToString());
                }
                return View(model);
            }
            await _userManager.AddToRolesAsync(user, model.roles.Where(r => r.IsSelect).Select(r => r.roleName));


            return RedirectToAction(nameof(Index));
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var user=await _userManager.FindByIdAsync(Id);
            if(user is null)return NotFound();

            var model = new EditUserViewModel
            { 
                Id = user.Id,
                UserName=user.UserName,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user ==null)return NotFound();

            if(await _userManager.FindByEmailAsync(model.Email)!=null)
            {
                ModelState.AddModelError("Email", "الايميل موجود بالفعل ");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "الاسم موجود سابقاً");
                return View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
