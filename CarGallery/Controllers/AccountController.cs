using CarGallery.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarGallery.Controllers
{
    public class AccountController : Controller
    {
        private CarGalleryContext _context;

        public AccountController(CarGalleryContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return View();

            var user = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email 
                                                        && x.Senha == usuario.Senha);
            if (user == null) 
            {
                ModelState.AddModelError("login_failed", "Usuário ou senha incorreta");
                return View();
            }

            var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email.ToString()));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                          new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return View();

            if (this._context.Usuarios.Any(x => x.Email == usuario.Email))
            {
                ModelState.AddModelError("email_in_use", "Email já esta sendo utilizado por outra pessoa");
                return View();
            }

            this._context.Add(usuario);
            this._context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
