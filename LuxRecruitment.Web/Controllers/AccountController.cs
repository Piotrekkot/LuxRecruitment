using LuxRecruitment.Web.Helpers;
using LuxRecruitment.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuxRecruitment.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly JwtTokenGenerator _tokenGenerator;

        public AccountController(JwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Username == "admin" && model.Password == "admin123")
            {
                var token = _tokenGenerator.GenerateToken(model.Username);

                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(60)
                });

                return RedirectToAction("Index", "ExchangeWeb");
            }

            ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login", "Account");
        }
    }
}
