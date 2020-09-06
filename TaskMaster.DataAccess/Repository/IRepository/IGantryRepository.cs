using System;
using System.Collections.Generic;
using System.Text;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository.IRepository
{
    public interface IGantryRepository : IRepository<Gantry>
    {
        void Update(Gantry gantry);
    }
}
