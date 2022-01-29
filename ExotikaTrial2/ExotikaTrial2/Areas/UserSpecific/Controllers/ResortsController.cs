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
using Microsoft.AspNetCore.Authorization;
using ExotikaTrial2.Utility;
using ExotikaTrial2.Models.ViewModels;
using System.Security.Claims;

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific")]
    [Authorize(Roles = SD.Role_User_Resort)]
    public class ResortsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResortsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult ResortOwnerDashboard()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var stats = new UserStatsVM()
            {
                registrations = _unitOfWork.ResortBooking.GetAll(u => u.ResortId == claim.Value).Count(),
            };
            return View(stats);
        }

        public IActionResult UpsertFeedback(string? forId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var fbk = _unitOfWork.Feedback.GetFirstOrDefault(u => u.forId == forId && u.byId == claim.Value, includeProperties: "vendor,resort");
            Feedback feedback = new();
            if (fbk == null)
            {
                feedback.forId = forId;
                feedback.byId = claim.Value;
                feedback.resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == claim.Value);
                feedback.vendor = _unitOfWork.Vendor.GetFirstOrDefault(u => u.VendorId == forId);
                return View(feedback);
            }
            else return View(fbk);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertFeedback(Feedback feedback)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                if (feedback.Id == 0)
                {
                    feedback.lastUpdated = DateTime.Now;
                    feedback.resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == feedback.byId);
                    feedback.vendor = _unitOfWork.Vendor.GetFirstOrDefault(u => u.VendorId == feedback.forId);
                    _unitOfWork.Feedback.Add(feedback);
                    _unitOfWork.Save();
                    TempData["Success"] = "Feedback Posted Successfully!";
                }
                else
                {
                    if (feedback.byId == claim.Value)
                    {
                        feedback.lastUpdated = DateTime.Now;
                        feedback.resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == feedback.byId);
                        feedback.vendor = _unitOfWork.Vendor.GetFirstOrDefault(u => u.VendorId == feedback.forId);
                        _unitOfWork.Feedback.Update(feedback);
                        _unitOfWork.Save();
                        TempData["Success"] = "Feedback Updated Successfully!";
                    }
                }
                return RedirectToAction("OngoingContracts", "Requirements");   //return RedirectToAction("Index", "ControllerName"); if we want to go to some other controller
            }
            TempData["Error"] = "An error occured. Please try later.";
            return View(feedback);
        }



        public IActionResult TouristFeedbacks()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var feedbackList = _unitOfWork.Feedback.GetAll(u => u.forId == claim.Value, includeProperties: "tourist,resort");
            if (feedbackList == null)
            {
                return NotFound();
            }
            return View(feedbackList);
        }

    }
}
