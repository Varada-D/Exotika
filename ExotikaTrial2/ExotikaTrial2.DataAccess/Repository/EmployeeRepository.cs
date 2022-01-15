using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExotikaTrial2.Data;

namespace ExotikaTrial2.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ExotikaTrial2Context _db;

        public EmployeeRepository(ExotikaTrial2Context db): base(db)
        {
            _db = db;
        }
        public void Update(Employee obj)
        {
            _db.Employees.Update(obj);
        }
    }
}
