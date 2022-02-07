using ExotikaTrial2.Data;
using ExotikaTrial2.DataAccess.Repository;
using ExotikaTrial2.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employee { get; private set; }
        public IResortRepository Resort { get; private set; }
        public IVendorRepository Vendor { get; private set; }
        public ITouristRepository Tourist { get; private set; }
        public IAdminRepository Admin { get; private set; }
        public IHandicraftDealerRepository HandicraftDealer { get; private set; }
        public IBookRepository Bookings { get; private set; }
        public IResortBookingRepository ResortBooking { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IContractRepository Contract { get; private set; }
        public IPackageRepository Package { get; private set; }
        public IRequirementRepository Requirement { get; private set; }
        public IFeedbackRepository Feedback { get; private set; }

        private ExotikaTrial2Context _db;
        public UnitOfWork(ExotikaTrial2Context db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            Resort = new ResortRepository(_db);
            Vendor = new VendorRepository(_db);
            Tourist = new TouristRepository(_db);
            HandicraftDealer = new HandicraftDealerRepository(_db);
            Admin = new AdminRepository(_db);
            ResortBooking = new ResortBookingRepository(_db);
            Bookings = new BookRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Contract = new ContractRepository(_db);
            Package = new PackageRepository(_db);
            Feedback = new FeedbackRepository(_db);
            Requirement = new RequirementRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
