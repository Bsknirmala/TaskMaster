using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMaster.DataAccess.Data;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository
{
    public class GantryRepository : Repository<Gantry>, IGantryRepository
    {
        private readonly ApplicationDbContext _db;

        public GantryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Gantry gantry)
        {
            var objFromDb = _db.Gantries.FirstOrDefault(s => s.Id == gantry.Id);
            if (objFromDb != null)
            {
                objFromDb.Description = gantry.Description;
                objFromDb.ts = gantry.ts;
                objFromDb.df = gantry.df;
            }
        }
    }
}
