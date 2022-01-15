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
                tourist = _unitOfWork.ApplicationUser.GetAll(u=>u.Role==SD.Role_User_Tourist).Count(),
                resort = _unitOfWork.ApplicationUser.GetAll(u=>u.Role==SD.Role_User_Resort).Count(),
                vendor = _unitOfWork.ApplicationUser.GetAll(u=>u.Role==SD.Role_User_Vendor).Count(),
                handicraftDealer = _unitOfWork.ApplicationUser.GetAll(u=>u.Role==SD.Role_User_HandicraftDealer).Count()
            };
            return View(s);
        }

        public IActionResult UserSpecificStats(string userRole)
        {            
            if (userRole == SD.Role_User_Tourist)
            {
                UserStatsVM stat = new UserStatsVM()
                {
                    visitors=0,
                    registrations = _unitOfWork.Tourist.GetAll().Count(),
                    bookings = _unitOfWork.ResortBooking.GetAll().Count()
                };
            }
            else
            {
                if (userRole == SD.Role_User_Resort)
                {
                    UserStatsVM stat = new UserStatsVM()
                    {
                        visitors=0,
                        registrations = _unitOfWork.Resort.GetAll().Count(),
                        bookings = _unitOfWork.ResortBooking.GetAll().Count()
                    };
                }
                else
                {
                    if (userRole == SD.Role_User_Vendor)
                    {
                        UserStatsVM stat = new UserStatsVM()
                        {
                            visitors=0,
                            registrations = _unitOfWork.Vendor.GetAll().Count(),
                            bookings = _unitOfWork.Contract.GetAll().Count()
                        };
                    }
                    else
                    {
                        if (userRole == SD.Role_User_HandicraftDealer)
                        {
                            UserStatsVM stat = new UserStatsVM()
                            {
                                visitors = 0,
                                registrations = _unitOfWork.HandicraftDealer.GetAll().Count(),
                                bookings = 0
                            };
                        }
                    }
                }
            }
            return View(userRole);
        }
    }
}
