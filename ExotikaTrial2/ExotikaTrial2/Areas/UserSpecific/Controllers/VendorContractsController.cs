using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExotikaTrial2.Data;
using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using System.Security.Claims;
using ExotikaTrial2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ExotikaTrial2.Utility;

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific"), Authorize(Roles = SD.Role_User_Vendor)]
    public class VendorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult VendorDashboard()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var stats = new UserStatsVM()
            {
                registrations = _unitOfWork.Contract.GetAll( u => u.VendorId == claim.Value && u.Status == SD.Proposal_Given ).Count(),
                bookings = _unitOfWork.Contract.GetAll( u => u.VendorId == claim.Value && u.Status == SD.Proposal_Accepted ).Count()
            };
            return View(stats);
        }
    }
}
