using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;
using TaskMaster.Models.ViewModels;
using TaskMaster.Utility;

namespace TaskMaster.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            UserVM userVM = new UserVM()
            {
               User = new User(),
            };
            if (id == null)
            {
                //UserVM.Password = UserVM.User.Password;
                // this is for create
                return View(userVM);
            }
            // this is for edit

            userVM.User = _unitOfWork.User.Get(id.GetValueOrDefault()); ;
            if (userVM.User == null)
            {
                return NotFound();
            }
            return View(userVM);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public IActionResult Upsert(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", userVM.User.Name);
                var objFromDb = _unitOfWork.SP_Call.OneRecord<User>(SD.Proc_Logindetail_by_name, parameter);
                if (objFromDb != null)
                {
                     if (userVM.ConfirmPassword == userVM.User.Password)
                    {
                        if (userVM.User.Id == 0)
                        {
                            _unitOfWork.User.Add(userVM.User);

                        }
                        else
                        {
                            _unitOfWork.User.Update(userVM.User);
                        }
                        _unitOfWork.Save();
                        //ViewBag.SuccessMessage = "Registration successful";
                        ViewBag.Message = "Registration successful";

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        //ViewBag.SuccessMessage = "Password mismatch.Recheck";
                        ModelState.AddModelError("", "Passwords do not match");
                        return View(userVM.User);
                    }
                }
            }
            return View(userVM.User);
        }

        #region API CALLS


        [HttpGet]

        public IActionResult GetAll()
        {

            var allObj = _unitOfWork.User.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.User.Get(id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.User.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}