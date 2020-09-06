using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;
using TaskMaster.Models.ViewModels;
using TaskMaster.Utility;
using Microsoft.AspNetCore.Http;

namespace TaskMaster.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class LoginController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

  
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserVM uservm)

        { 

            //if (ModelState.IsValid)
            //{
                var parameter = new DynamicParameters();
                parameter.Add("@Name", uservm.User.Name);
                parameter.Add("@Password", uservm.User.Password);
                var objFromDb = _unitOfWork.SP_Call.OneRecord<User>(SD.Proc_loginvalidation, parameter);
                if (objFromDb != null)
                {
                     HttpContext.Session.SetString("Username", uservm.User.Name);
                     HttpContext.Session.SetString("Role", objFromDb.AccessLevel);
                     return RedirectToAction("Index","Home",new { Area = "Customer", id = objFromDb.Id });
                }
                else
                {
                        ModelState.AddModelError("", "Username / Password provided is incorrect.");
                }
            return View(uservm);
          }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        }
}