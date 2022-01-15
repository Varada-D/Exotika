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
    public class VendorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Vendors
        public IActionResult Index()
        {
            return View();
        }

        // GET: Vendors/Details/5
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = _unitOfWork.Vendor
                .GetFirstOrDefault(m => m.VendorId == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Vendor.Add(vendor);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = _unitOfWork.Vendor.GetFirstOrDefault(u=>u.VendorId==id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                    _unitOfWork.Vendor.Update(vendor);
                    _unitOfWork.Save();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!VendorExists(vendor.VendorId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = _unitOfWork.Vendor
                .GetFirstOrDefault(m => m.VendorId == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var vendor = _unitOfWork.Vendor.GetFirstOrDefault(u=>u.VendorId==id);
            _unitOfWork.Vendor.Remove(vendor);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(string id)
        {
            var vend = _unitOfWork.Vendor.GetFirstOrDefault(e => e.VendorId == id);
            if (vend != null)
            {
                return true;
            }
            return false;
        }
    }
}
