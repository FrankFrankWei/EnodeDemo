using EQueue.AdminWeb.Models;
using EQueue.AdminWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EQueue.AdminWeb.Controllers
{
    public class AdminController : Controller
    {
        private IAuthenticationService _authService;
        public AdminController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var settingPassword = System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];
                if (model.UserName.ToLower() == "administrator" && Md5(model.Password).Equals(settingPassword))
                {
                    _authService.SignIn(model.UserName, model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "登录名或者密码错误";

                }
            }
            else
            {
                ViewBag.Message = "请输入登录名和密码";
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _authService.SignOut();
            return RedirectToAction("Login");
        }

        private string Md5(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }


}