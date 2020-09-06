using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMaster.DataAccess.Data;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository
{
    public class HardwaretypeRepository : Repository<Hardwaretype>, IHardwaretypeRepository
    {
        private readonly ApplicationDbContext _db;

        public HardwaretypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Hardwaretype hardwaretype)
        {
            var objFromDb = _db.Hardwaretypes.FirstOrDefault(s => s.Id == hardwaretype.Id);
            if (objFromDb != null)
            {
                objFromDb.Description = hardwaretype.Description;
                objFromDb.ts = hardwaretype.ts;
                objFromDb.df = hardwaretype.df;
            }
        }
    }
}
