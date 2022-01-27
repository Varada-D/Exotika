using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models.ViewModels;
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExotikaTrial2.Areas.UserSpecific.Controllers
{
    [Area("UserSpecific")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            StatsVM s = new StatsVM ()
            {
                tourist = _unitOfWork.Tourist.GetAll().Count(),
                resort = _unitOfWork.Resort.GetAll().Count(),
                vendor = _unitOfWork.Vendor.GetAll().Count(),
                handicraftDealer = _unitOfWork.HandicraftDealer.GetAll().Count(),
            };
            return View(s);
        }

        public IActionResult TouristStats()
        {
            UserStatsVM touristStats = new UserStatsVM()
            {
                visitors = 0,
                registrations = _unitOfWork.Tourist.GetAll().Count(),
                bookings = _unitOfWork.ResortBooking.GetAll().Count()
            };
            return View(touristStats);
        }

        public IActionResult ResortStats()
        {
            UserStatsVM resortStats = new UserStatsVM()
            {
                visitors = 0,
                registrations = _unitOfWork.Tourist.GetAll().Count(),
                bookings = _unitOfWork.ResortBooking.GetAll().Count()
            };
            return View(resortStats);
        }

        public IActionResult VendorStats()
        {
            UserStatsVM vendorStats = new UserStatsVM()
            {
                visitors = 0,
                registrations = _unitOfWork.Tourist.GetAll().Count(),
                bookings = _unitOfWork.ResortBooking.GetAll().Count()
            };
            return View(vendorStats);
        }

        public IActionResult HandicraftDealerStats()
        {
            UserStatsVM handicraftDealerStats = new UserStatsVM()
            {
                visitors = 0,
                registrations = _unitOfWork.Tourist.GetAll().Count(),
                bookings = _unitOfWork.ResortBooking.GetAll().Count()
            };
            return View(handicraftDealerStats);
        }
    }
}
