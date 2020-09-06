using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskMaster.Models.ViewModels;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskMaster.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class GantryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public GantryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            GantryVM gantryVM = new GantryVM()
            {
                Gantry = new Gantry(),
                CarparkList = _unitOfWork.Carpark.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                // this is for create
                return View(gantryVM);
            }
            // this is for edit

            gantryVM.Gantry = _unitOfWork.Gantry.Get(id.GetValueOrDefault());
            if (gantryVM.Gantry == null)
            {
                return NotFound();
            }
            return View(gantryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(GantryVM gantryVM)
        {
            if (ModelState.IsValid)

            {
                if (gantryVM.Gantry.Id == 0)
                {
                    _unitOfWork.Gantry.Add(gantryVM.Gantry);
                }
                else
                {
                    _unitOfWork.Gantry.Update(gantryVM.Gantry);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                gantryVM.CarparkList = _unitOfWork.Carpark.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                });
                if (gantryVM.Gantry.Id != 0)
                {
                    gantryVM.Gantry = _unitOfWork.Gantry.Get((int)gantryVM.Gantry.Id);
                }
            }
            return View(gantryVM);
        }

    

    #region API CALLS


    [HttpGet]

    public IActionResult GetAll()
    {

        var allObj = _unitOfWork.Gantry.GetAll(includeProperties: "Carpark");
        return Json(new { data = allObj });
    }


    [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.Gantry.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Gantry.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}