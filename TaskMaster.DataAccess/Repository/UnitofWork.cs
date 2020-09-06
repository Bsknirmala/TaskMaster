using System;
using System.Collections.Generic;
using System.Text;
using TaskMaster.DataAccess.Data;
using TaskMaster.DataAccess.Repository.IRepository;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Carpark = new CarparkRepository(_db);
            Gantry = new GantryRepository(_db);
            Hardwaretype = new HardwaretypeRepository(_db);
            User = new UserRepository(_db);
            Issue = new IssueRepository(_db);
            SP_Call = new SP_Call(_db);
        }


        public ICarparkRepository Carpark { get; private set; }

        public IGantryRepository Gantry { get; private set; }

        public IHardwaretypeRepository Hardwaretype { get; private set; }

        public IUserRepository User { get; private set; }

        public IIssueRepository Issue { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
