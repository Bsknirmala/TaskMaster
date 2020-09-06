using System;
using System.Collections.Generic;
using System.Text;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository.IRepository
{
    public interface IHardwaretypeRepository : IRepository<Hardwaretype>
    {
        void Update(Hardwaretype hardwaretype);
    }
}
