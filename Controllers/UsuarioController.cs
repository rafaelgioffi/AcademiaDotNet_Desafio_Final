using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeEncomendas.Models;
using System.Security.Claims;

namespace SistemaDeEncomendas.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Encomendas");
            }
            return View();
        }

        [HttpPost]
        public IActionResult LoginPage(Usuario usuario, [FromServices] Context context)
        {
            var login = usuario.Login;
            var senha = usuario.Senha;
            var loginBanco = (from l in context.Usuario.Where(lo => lo.Login == login && lo.Senha == senha) select l).FirstOrDefault();

            try
            {
                if (ModelState.IsValid)
                {
                    if (loginBanco != null)
                    {
                        Login(usuario);
                        //return RedirectToAction("UserPage");
                        return RedirectToAction("Index", "Encomendas");
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário e/ou senha inválido!";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro ao autenticar. Tente novamente!";
            }
            return View();
        }

        private async void Login(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var idUser = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(idUser);

            var propsAuth = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, propsAuth);
        }

        [Authorize]
        public IActionResult UserPage()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("LoginPage");
        }

    }
}
