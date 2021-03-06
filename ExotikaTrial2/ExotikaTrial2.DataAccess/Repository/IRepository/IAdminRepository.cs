using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.DataAccess.Repository.IRepository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        void Update(Admin obj);
    }
}
