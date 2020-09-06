using System;
using System.Collections.Generic;
using System.Text;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository.IRepository
{
    public interface ICarparkRepository : IRepository<Carpark>
    {
        void Update(Carpark carpark);
    }
}
