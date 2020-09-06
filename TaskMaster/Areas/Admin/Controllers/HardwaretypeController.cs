using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class HardwaretypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public HardwaretypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Hardwaretype hardwaretype = new Hardwaretype();
            if (id == null)
            {
                // this is for create
                return View(hardwaretype);
            }
            // this is for edit

            hardwaretype = _unitOfWork.Hardwaretype.Get(id.GetValueOrDefault()); ;
            if (hardwaretype == null)
            {
                return NotFound();
            }
            return View(hardwaretype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Hardwaretype hardwaretype)
        {
            if (ModelState.IsValid)
            {
                if (hardwaretype.Id == 0)
                {
                    _unitOfWork.Hardwaretype.Add(hardwaretype);

                }
                else
                {
                    _unitOfWork.Hardwaretype.Update(hardwaretype);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(hardwaretype);
        }

        #region API CALLS


        [HttpGet]

        public IActionResult GetAll()
        {

            var allObj = _unitOfWork.Hardwaretype.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.Hardwaretype.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Hardwaretype.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}