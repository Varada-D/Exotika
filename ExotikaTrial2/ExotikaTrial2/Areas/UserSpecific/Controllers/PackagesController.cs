using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExotikaTrial2.Data;
using ExotikaTrial2.Models;
using ExotikaTrial2.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using ExotikaTrial2.Utility;
using System.Security.Claims;

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific")]
    [Authorize(Roles = SD.Role_User_Resort)]
    public class PackagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PackagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Packages
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var packagesList = _unitOfWork.Package.GetAll(u => u.ResortId == claim.Value);
            return View(packagesList);
        }

        // GET: Packages/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = _unitOfWork.Package
                .GetFirstOrDefault(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Packages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {

            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                package.ResortId = claim.Value;
                _unitOfWork.Package.Add(package);
                _unitOfWork.Save();
                TempData["Success"] = "Package Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Error Adding Package";
            return View(package);
        }

        // GET: Packages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = _unitOfWork.Package.GetFirstOrDefault(u=>u.PackageId==id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Package package)
        {
            if (id != package.PackageId)
            {
                TempData["Error"] = "Error Updating Package Details";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Package.Update(package);
                    _unitOfWork.Save();
                    TempData["Success"] = "Package Details Updated Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["Error"] = "Error Updating Package Details";
                    if (!PackageExists(package.PackageId))
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
            return View(package);
        }

        // GET: Packages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = _unitOfWork.Package
                .GetFirstOrDefault(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var package =  _unitOfWork.Package.GetFirstOrDefault(u=>u.PackageId==id);
            _unitOfWork.Package.Remove(package);
            _unitOfWork.Save();
            TempData["Success"] = "Package Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            var pkg = _unitOfWork.Package.GetFirstOrDefault(e => e.PackageId == id);
            if (pkg != null)
            {
                return true;
            }
            return false;
        }
    }
}
