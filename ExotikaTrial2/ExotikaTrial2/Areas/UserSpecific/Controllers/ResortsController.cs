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
    public class ResortsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResortsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Resorts
        public IActionResult Index()
        {
            return View();
        }

        // GET: Resorts/Details/5
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resort = _unitOfWork.Resort
                .GetFirstOrDefault(m => m.ResortId == id);
            if (resort == null)
            {
                return NotFound();
            }

            return View(resort);
        }

        // GET: Resorts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resorts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Resort resort)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Resort.Add(resort);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(resort);
        }

        // GET: Resorts/Edit/5
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resort = _unitOfWork.Resort.GetFirstOrDefault(u=>u.ResortId==id);
            if (resort == null)
            {
                return NotFound();
            }
            return View(resort);
        }

        // POST: Resorts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Resort resort)
        {
            if (id != resort.ResortId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                    _unitOfWork.Resort.Update(resort);
                    _unitOfWork.Save();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ResortExists(resort.ResortId))
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
            return View(resort);
        }

        // GET: Resorts/Delete/5
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resort = _unitOfWork.Resort
                .GetFirstOrDefault(m => m.ResortId == id);
            if (resort == null)
            {
                return NotFound();
            }

            return View(resort);
        }

        // POST: Resorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var resort = _unitOfWork.Resort.GetFirstOrDefault(u=>u.ResortId==id);
            _unitOfWork.Resort.Remove(resort);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ResortExists(string id)
        {
            var res = _unitOfWork.Resort.GetFirstOrDefault(e => e.ResortId == id);
            if (res != null)
            {
                return true;
            }
            return false;
        }
    }
}
