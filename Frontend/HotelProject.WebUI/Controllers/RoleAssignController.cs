using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.WebUI.Controllers
{
    public class RoleAssignController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleAssignController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values=_userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            string userRoleName = userRoles.FirstOrDefault(); // Kullanıcının mevcut rolü

            // View’a sadece RoleID ve RoleName gönderiyoruz
            List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();
            foreach (var item in roles)
            {
                roleAssignViewModels.Add(new RoleAssignViewModel
                {
                    RoleID = item.Id,
                    RoleName = item.Name
                });
            }

            ViewData["UserRole"] = userRoleName; // Seçili rolü ViewData ile gönder
            ViewData["UserId"] = user.Id;        // Hidden input için
            return View(roleAssignViewModels);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(int UserId, int SelectedRoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());

            // Kullanıcının mevcut rollerini temizle
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Seçilen rolü ekle
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == SelectedRoleId);
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }

            return RedirectToAction("Index");
        }



    }
}
