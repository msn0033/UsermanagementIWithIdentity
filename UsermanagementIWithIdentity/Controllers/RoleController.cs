using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsermanagementIWithIdentity.ViewModel;

namespace UsermanagementIWithIdentity.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _roleManager.Roles.ToListAsync());
        }

        //public async Task<IActionResult> AddOrEdit(string? id)
        //{
        //    if (id == null)
        //        return View(new RoleFormViewModel());
        //    else
        //    {
        //        var tt = await _roleManager.FindByIdAsync(id);
        //        if (tt == null)
        //        { return View(NotFound()); }

        //        return View(tt);
        //    }
        //}

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleFormViewModel roleFormViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", _roleManager.Roles.ToListAsync());
            }


            if (await _roleManager.RoleExistsAsync(roleFormViewModel.Name))
            {
                ModelState.AddModelError("Name", "Role is Exists!");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }
            await _roleManager.CreateAsync(new IdentityRole(roleFormViewModel.Name.Trim()));
            return RedirectToAction(nameof(Index));
        }

    }
}
