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

    public class CarparkController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public CarparkController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Carpark carpark = new Carpark();
            if (id == null)
            {
                // this is for create
                return View(carpark);
            }
            // this is for edit
        
            carpark = _unitOfWork.Carpark.Get(id.GetValueOrDefault()); ;
            if (carpark == null)
            {
                return NotFound();
            }
            return View(carpark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Carpark carpark)
        {
            if (ModelState.IsValid)
            {
                if (carpark.Id == 0)
                {
                    _unitOfWork.Carpark.Add(carpark);

                }
                else
                {
                    _unitOfWork.Carpark.Update(carpark);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(carpark);
        }

        #region API CALLS


        [HttpGet]

        public IActionResult GetAll()
        {

            var allObj = _unitOfWork.Carpark.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.Carpark.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Carpark.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}