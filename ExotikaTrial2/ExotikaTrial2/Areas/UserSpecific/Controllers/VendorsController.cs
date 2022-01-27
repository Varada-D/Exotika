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
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific"), Authorize(Roles = SD.Role_User_Vendor)]
    public class VendorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult VendorDashboard()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var stats = new UserStatsVM()
            {
                registrations = _unitOfWork.Contract.GetAll(u => u.VendorId == claim.Value && u.Status == SD.Proposal_Given).Count(),
                bookings = _unitOfWork.Contract.GetAll(u => u.VendorId == claim.Value && u.Status == SD.Proposal_Accepted).Count()
            };
            return View(stats);
        }


        public IActionResult ContractList(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var contractList = _unitOfWork.Contract.GetAll(u => u.VendorId == claim.Value, includeProperties: "Requirement,Requirement.Resort");
            switch (status)
            {
                case "proposalSent":
                    contractList = contractList.Where(u => u.Status == SD.Proposal_Given);
                    break;
                case "proposalApproved":
                    contractList = contractList.Where(u => u.Status == SD.Proposal_Accepted);
                    break;
                case "completed":
                    contractList = contractList.Where(u => u.Status == SD.Contract_Completed);
                    break;
                default:
                    break;
            }
            return View(contractList);
        }

        public IActionResult ContractDetails(int? contractId)
        {
            if (contractId == null)
            {
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var contract = _unitOfWork.Contract.GetFirstOrDefault(u => u.ContractId == contractId && u.VendorId == claim.Value, includeProperties: "Requirement,Requirement.Resort");
            if(contract==null)
            {
                return NotFound();
            }
            return View(contract);
        }


        public IActionResult Feedbacks()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var feedbackList = _unitOfWork.Feedback.GetAll(u => u.forId == claim.Value, includeProperties:"vendor,resort");
            if (feedbackList == null)
            {
                return NotFound();
            }
            return View(feedbackList);
        }
    }
}
