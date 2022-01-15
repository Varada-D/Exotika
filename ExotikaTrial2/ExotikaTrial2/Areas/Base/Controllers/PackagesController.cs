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

namespace ExotikaTrial2.Controllers
{
    [Area("Base")]
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
            return View();
        }

        // GET: Packages/Details/5
        public IActionResult Details(string? id)
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
                _unitOfWork.Package.Add(package);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: Packages/Edit/5
        public IActionResult Edit(string? id)
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
        public IActionResult Edit(string id, Package package)
        {
            if (id != package.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                    _unitOfWork.Package.Update(package);
                    _unitOfWork.Save();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!PackageExists(package.PackageId))
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
            return View(package);
        }

        // GET: Packages/Delete/5
        public IActionResult Delete(string? id)
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
        public IActionResult DeleteConfirmed(string id)
        {
            var package =  _unitOfWork.Package.GetFirstOrDefault(u=>u.PackageId==id);
            _unitOfWork.Package.Remove(package);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(string id)
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
