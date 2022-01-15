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

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific")]
    public class ResortBookingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResortBookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ResortBookings
        public IActionResult Index()
        {
            return View();
        }

        // GET: ResortBookings/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resortBookings = _unitOfWork.ResortBooking
                .GetFirstOrDefault(m => m.BookingID == id);
            if (resortBookings == null)
            {
                return NotFound();
            }

            return View(resortBookings);
        }

        // GET: ResortBookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResortBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResortBookings resortBookings)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ResortBooking.Add(resortBookings);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(resortBookings);
        }

        // GET: ResortBookings/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resortBookings = _unitOfWork.ResortBooking.GetFirstOrDefault(u=>u.BookingID==id);
            if (resortBookings == null)
            {
                return NotFound();
            }
            return View(resortBookings);
        }

        // POST: ResortBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ResortBookings resortBookings)
        {
            if (id != resortBookings.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ResortBooking.Update(resortBookings);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResortBookingsExists(resortBookings.BookingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(resortBookings);
        }

        // GET: ResortBookings/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resortBookings = _unitOfWork.ResortBooking
                .GetFirstOrDefault(m => m.BookingID == id);
            if (resortBookings == null)
            {
                return NotFound();
            }

            return View(resortBookings);
        }

        // POST: ResortBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var resortBookings = _unitOfWork.ResortBooking.GetFirstOrDefault(u=>u.BookingID==id);
            _unitOfWork.ResortBooking.Remove(resortBookings);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ResortBookingsExists(int id)
        {
            var resBk = _unitOfWork.ResortBooking.GetFirstOrDefault(u => u.BookingID == id);
            if (resBk != null)
            {
                return true;
            }
            return false;
        }
    }
}
