using System;
using System.Collections.Generic;
using System.Text;
using TaskMaster.Models;

namespace TaskMaster.DataAccess.Repository.IRepository
{
    public interface IIssueRepository : IRepository<Issue>
    {
        void Update(Issue issue);
    }
}
