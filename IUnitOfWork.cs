using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepotManagementSystem
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
