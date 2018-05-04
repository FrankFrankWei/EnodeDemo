using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganizationBC.QueryServices;
using Google.Web.ViewModels;
using Google.Web.Extensions;
using Google.Infrastructure.Encrypts;
using Google.Web.Services;

namespace Google.Web.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly IEmployeeQueryService _employeeQueryService;
        private readonly IAuthenticationService _authenticationService;
        public AuthorizeController(IEmployeeQueryService employeeQueryService, IAuthenticationService authenticationService)
        {
            _employeeQueryService = employeeQueryService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult SignIn(LoginModel model)
        {
            var invalidResult = new CustomJsonResult(false, "用户名或者密码错误");
            if (!ModelState.IsValid)
            {
                return Json(invalidResult);
            }
            //_authenticationService.SignIn("11111", model.UserName, false);
            //return Json(new CustomJsonResult(true, "登录成功"));
            var employee = _employeeQueryService.FindByUserName(model.UserName);
            if (employee == null)
            {
                return Json(invalidResult);
            }
            else
            {
                if (!PasswordHash.ValidatePassword(model.Password, employee.Password))
                {
                    return Json(invalidResult);
                }
                else
                {
                    _authenticationService.SignIn(employee.Id, model.UserName, false);
                    return Json(new CustomJsonResult(true, "登录成功"));
                }
            }
        }
        
        public ActionResult SignOut()
        {
            _authenticationService.SignOut();
            return Json(new CustomJsonResult(true, "退出成功"));
        }


    }
}