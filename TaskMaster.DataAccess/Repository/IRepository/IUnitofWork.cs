using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMaster.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        ICarparkRepository Carpark{ get; }
        IGantryRepository Gantry { get; }

        IHardwaretypeRepository Hardwaretype { get; }

        IUserRepository User { get; }

        IIssueRepository Issue { get; }

        ISP_Call SP_Call { get; }
        void Save();
    }
}
