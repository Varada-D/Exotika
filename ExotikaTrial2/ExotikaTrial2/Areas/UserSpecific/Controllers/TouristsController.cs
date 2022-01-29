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

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific")]
    public class TouristsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TouristsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Tourists
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userDetails = _unitOfWork.Tourist.GetFirstOrDefault(u => u.TouristId == claim.Value);
            return View(userDetails);
        }

        public IActionResult BookingHistory()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var list = _unitOfWork.ResortBooking.GetAll(u => u.TouristId == claim.Value, includeProperties: "Package,Resort,TouristDetails");
            return View(list);
        }

        public IActionResult BookingDetail(int? bookingId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var detail = _unitOfWork.ResortBooking.GetFirstOrDefault(u => u.BookingID == bookingId && u.TouristId == claim.Value, includeProperties: "Package,Resort,TouristDetails");
            return View(detail);
        }

        public IActionResult UpsertFeedback(string? forId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var fbk = _unitOfWork.Feedback.GetFirstOrDefault(u => u.forId == forId && u.byId == claim.Value, includeProperties: "tourist,resort");
            Feedback feedback = new();
            if (fbk == null)
            {
                feedback.forId = forId;
                feedback.byId = claim.Value;
                feedback.resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == forId);
                feedback.tourist = _unitOfWork.Tourist.GetFirstOrDefault(u => u.TouristId == claim.Value);
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
                    feedback.resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == feedback.forId);
                    feedback.tourist = _unitOfWork.Tourist.GetFirstOrDefault(u => u.TouristId == feedback.byId);
                    _unitOfWork.Feedback.Add(feedback);
                    _unitOfWork.Save();
                    TempData["Success"] = "Feedback Posted Successfully!";
                }
                else
                {
                    if (feedback.byId == claim.Value)
                    {
                        feedback.lastUpdated = DateTime.Now;
                        feedback.resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == feedback.forId);
                        feedback.tourist = _unitOfWork.Tourist.GetFirstOrDefault(u => u.TouristId == feedback.byId);
                        _unitOfWork.Feedback.Update(feedback);
                        _unitOfWork.Save();
                        TempData["Success"] = "Feedback Updated Successfully!";
                    }
                }
                return RedirectToAction("BookingHistory");   //return RedirectToAction("Index", "ControllerName"); if we want to go to some other controller
            }
            TempData["Error"] = "An error occured. Please try later.";
            return View(feedback);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var bookingList = _unitOfWork.ResortBooking.GetAll(u=>u.TouristId==claim.Value, includeProperties: "Package,Resort,TouristDetails");
            return Json(new { data = bookingList });
        }
        #endregion
    }
}
