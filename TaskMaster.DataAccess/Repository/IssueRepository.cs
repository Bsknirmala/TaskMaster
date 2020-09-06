using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMaster.DataAccess.Data;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository
{
    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly ApplicationDbContext _db;

        public IssueRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Issue issues)
        {
            var objFromDb = _db.Issues.FirstOrDefault(s => s.Id == issues.Id);
            if (objFromDb != null)
            {
                objFromDb.Description= issues.Description;
                objFromDb.Reportedby = issues.Reportedby;
                objFromDb.Issuedt = issues.Issuedt;
                objFromDb.Action = issues.Action;
                objFromDb.ServiceRptno = issues.ServiceRptno;
                objFromDb.Issuestatus = issues.Issuestatus;
                objFromDb.CarparkID = issues.CarparkID;
                objFromDb.GantryID = issues.GantryID;
                objFromDb.CategoryID = issues.CategoryID;
                objFromDb.ts = issues.ts;
                objFromDb.df = issues.df;
            }
        }
    }
}
