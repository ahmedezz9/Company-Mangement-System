using Company.G02.DAL.Models;
using Company.G02.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManger;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManger)
        {
            _userManager = userManager;
            _signInManger = signInManger;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree
                        };
                    }
                }
               var result =  await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction("SignIn");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                
                var user =await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        await _signInManger.PasswordSignInAsync(user, model.Password,model.RememberMe,false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                
            }
            return View(model);
            
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
           await _signInManger.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
