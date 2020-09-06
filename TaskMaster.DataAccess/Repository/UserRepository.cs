using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskMaster.DataAccess.Data;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            var objFromDb = _db.Login.FirstOrDefault(s => s.Id == user.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = user.Name;
                objFromDb.Password = user.Password;
                objFromDb.AccessLevel = user.AccessLevel;
                objFromDb.Dept = user.Dept;
                objFromDb.ts = user.ts;
                objFromDb.df = user.df;
            }
        }
    }
}
