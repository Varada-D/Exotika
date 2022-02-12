using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IResortRepository Resort { get; }
        IVendorRepository Vendor { get; }
        ITouristRepository Tourist { get; }
        IAdminRepository Admin { get; }
        IHandicraftDealerRepository HandicraftDealer { get; }
        IBookRepository Bookings { get; }
        IResortBookingRepository ResortBooking { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IContractRepository Contract { get; }
        IRequirementRepository Requirement { get; }
        IPackageRepository Package { get; }
        IFeedbackRepository Feedback { get; }
        IProductRepository Product { get; }

        void Save();
    }
}
