using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMaster.DataAccess.Data;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository
{
    public class CarparkRepository : Repository<Carpark>, ICarparkRepository
    {
        private readonly ApplicationDbContext _db;

        public CarparkRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Carpark carpark)
        {
            var objFromDb = _db.Carparks.FirstOrDefault(s => s.Id == carpark.Id);
            if (objFromDb != null)
            {
                objFromDb.Description = carpark.Description;
                objFromDb.Prefix = carpark.Prefix;
                objFromDb.ts = carpark.ts;
                objFromDb.df = carpark.df;
            }
        }
    }
}
