using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recodme.RD.FullStoQReborn.BusinessLayer.Person;
using Recodme.RD.FullStoQReborn.DataLayer.Person;
using System;
using System.Threading.Tasks;
using WebAPI.Models.HtmlComponents;
using WebAPI.Models.PersonViewModel;
using WebAPI.Support;

namespace WebAPI.Controllers
{
    public class AccountsController : Controller
    {
        private UserManager<FullStoqUser> UserManager { get; set; }
        private SignInManager<FullStoqUser> SignInManager { get; set; }
        private RoleManager<FullStoqRole> RoleManager { get; set; }
        
        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index), "Home");
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index), "Home");
        }

        public AccountsController(UserManager<FullStoqUser> uManager, SignInManager<FullStoqUser> sManager, RoleManager<FullStoqRole> rManager)
        {
            UserManager = uManager;
            SignInManager = sManager;
            RoleManager = rManager;
        }

        [HttpPost("/GenerateToken")]
        public IActionResult GenerateToken(LoginViewModel vm)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var accountBo = new AccountBusinessController(UserManager, RoleManager);
            var profile = new Profile(vm.VatNumber, vm.FirstName, vm.LastName, vm.PhoneNumber, vm.BirthDate);
            var registerOperation = await accountBo.Register(vm.UserName, vm.Email, vm.Password, profile, vm.Role);
            if (registerOperation.Success)
                return OperationSuccess("The account was successfuly registered!");
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, registerOperation.Message);
            return View(vm);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var loginOperation = await SignInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (loginOperation.Succeeded) return OperationSuccess("Welcome User");
            else
            {
                TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, loginOperation.ToString());
                return View(vm);
            }
        }


        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
