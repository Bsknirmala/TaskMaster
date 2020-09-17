using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;
using TaskMaster.Models.ViewModels;
using TaskMaster.Utility;
using Microsoft.AspNetCore.Http;

namespace TaskMaster.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class IssueController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public IssueController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index","Login");
            }

        }
        public IActionResult Upsert(int? id)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {

                IssueVM issuesVM = new IssueVM()
                {
                    Issue = new Issue(),
                    CarparkList = _unitOfWork.Carpark.GetAll().Select(i => new SelectListItem
                    {
                        Text = i.Description,
                        Value = i.Id.ToString()
                    }),

                    CategoryList = _unitOfWork.Hardwaretype.GetAll().Select(i => new SelectListItem
                    {
                        Text = i.Description,
                        Value = i.Id.ToString()
                    })
                };
                if (id == null)
                {
                    // this is for create
                    return View(issuesVM);
                }
                // this is for edit

                issuesVM.Issue = _unitOfWork.Issue.Get(id.GetValueOrDefault());
                if (issuesVM.Issue == null)
                {
                    return NotFound();
                }
                issuesVM.CarparkID = issuesVM.Issue.CarparkID;
                issuesVM.GantryID = issuesVM.Issue.GantryID;
                issuesVM.CategoryID = issuesVM.Issue.CategoryID;
                var parameter = new DynamicParameters();
                parameter.Add("@CarparkID", issuesVM.CarparkID);

                issuesVM.GantryList = _unitOfWork.SP_Call.List<Gantry>(SD.Proc_Gantrydetails_by_carpark, parameter).Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                });

                return View(issuesVM);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult GetGantryList(int CarparkId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@CarparkID", CarparkId);
            IssueVM issuesVM = new IssueVM()
            {
                Issue = new Issue(),
                GantryList = _unitOfWork.SP_Call.List<Gantry>(SD.Proc_Gantrydetails_by_carpark, parameter).Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                })
               
            };
            //return View();
            ViewBag.GantryOptions = issuesVM.GantryList;
            return PartialView("_GantryOptionPartial");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(IssueVM issuesVM)
        {
            if (ModelState.IsValid)

            {
                if (issuesVM.Issue.Id == 0)
                {
                    issuesVM.Issue.CarparkID = issuesVM.CarparkID;
                    issuesVM.Issue.GantryID = issuesVM.GantryID;
                    issuesVM.Issue.Loginuser = HttpContext.Session.GetString("Username");
                    //issuesVM.Issue.CategoryID= issuesVM.CategoryID;
                    _unitOfWork.Issue.Add(issuesVM.Issue);
                }
                else
                {
                    issuesVM.Issue.CarparkID = issuesVM.CarparkID;
                    var objFromDb = _unitOfWork.Issue.Get(issuesVM.Issue.Id);
                    if (objFromDb != null)
                        //issuesVM.Issue.Loginuser = HttpContext.Session.GetString("Username");
                        //issuesVM.Issue.GantryID = issuesVM.GantryID;
                        issuesVM.Issue.Loginuser = objFromDb.Loginuser;
                        issuesVM.Issue.Reportedby = objFromDb.Reportedby;
                        issuesVM.Issue.Issuedt = objFromDb.Issuedt;
                        _unitOfWork.Issue.Update(issuesVM.Issue);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                issuesVM.CarparkList = _unitOfWork.Carpark.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                });
                issuesVM.GantryList = _unitOfWork.Gantry.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                });
                issuesVM.CategoryList = _unitOfWork.Hardwaretype.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                });
                if (issuesVM.Issue.Id != 0)
                {

                    issuesVM.Issue = _unitOfWork.Issue.Get((int)issuesVM.Issue.Id);
                }
                else
                {
                    //issuesVM.CarparkID = issuesVM.Issue.CarparkID;
                    //issuesVM.GantryID = issuesVM.Issue.GantryID;
                    var parameter = new DynamicParameters();
                    parameter.Add("@CarparkID", issuesVM.CarparkID);
                    issuesVM.GantryList = _unitOfWork.SP_Call.List<Gantry>(SD.Proc_Gantrydetails_by_carpark, parameter).Select(i => new SelectListItem
                    {
                        Text = i.Description,
                        Value = i.Id.ToString()
                    });
                    issuesVM.GantryID = issuesVM.GantryID;
                    issuesVM.CategoryID = issuesVM.Issue.CategoryID;
                }
            }
            return View(issuesVM);
        }

        #region API CALLS


        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Issue.GetAll(includeProperties: "Carpark,Gantry,Hardwaretype");
            return Json(new { data = allObj });
        }

        public IActionResult GetIssueList(string status)
        {
            IEnumerable<Issue> IssueList;

            IssueList = _unitOfWork.Issue.GetAll(includeProperties: "Carpark,Gantry,Hardwaretype");
            switch (status)
            {
                case "all":
                    IssueList = _unitOfWork.Issue.GetAll(includeProperties: "Carpark,Gantry,Hardwaretype");
                    break;
                case "techfollowup":
                    IssueList = IssueList.Where(o => o.Issuestatus == "TechFollowup");
                    break;
                case "closed":
                    IssueList = IssueList.Where(o => o.Issuestatus == "Closed");
                    break;
                default:
                    IssueList = IssueList.Where(o => o.Issuestatus == "OpsFollowup");
                    break;
            }

            return Json(new { data = IssueList });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.Issue.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Issue.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}