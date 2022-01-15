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
            return View();
        }

        public IActionResult BookingHistory()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var list = _unitOfWork.ResortBooking.GetAll(u => u.TouristId.ToString() == claim.Value);
            return View(list);
        }

        public IActionResult Feedback(string? forID)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Feedback feedback = new();
            if (forID == null) //|| forID == 0
            {
                return View(feedback);
            }
            else
            {
                feedback = _unitOfWork.Feedback.GetFirstOrDefault(u => u.forId == forID && u.byId.ToString() == claim.Value);
                return View(feedback);
            }
        }

        //POST
        [HttpPost]
        [ActionName("Feedback")]
        [ValidateAntiForgeryToken]
        public IActionResult FeedbackPOST(Feedback obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Feedback.Add(obj);
                    // TempData["Success"] = "Company Created Successfully!";
                }
                else
                {
                    _unitOfWork.Feedback.Update(obj);
                    // TempData["Success"] = "Company Updated Successfully!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");   //return RedirectToAction("Index", "ControllerName"); if we want to go to some other controller
            }
            return View(obj);
        }
    }
}
