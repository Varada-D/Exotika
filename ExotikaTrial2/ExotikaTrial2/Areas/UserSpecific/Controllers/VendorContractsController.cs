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
    public class VendorContractsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorContractsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: VendorContracts
        public IActionResult Index()
        {
            return View();
        }

        // GET: VendorContracts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorContracts = _unitOfWork.Contract
                .GetFirstOrDefault(m => m.ContractId == id);
            if (vendorContracts == null)
            {
                return NotFound();
            }

            return View(vendorContracts);
        }

        // GET: VendorContracts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Contract.Add(contract);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        // GET: VendorContracts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract =  _unitOfWork.Contract.GetFirstOrDefault(u=>u.ContractId==id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: VendorContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contract contract)
        {
            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Contract.Update(contract);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorContractsExists(contract.ContractId))
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
            return View(contract);
        }

        // GET: VendorContracts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorContracts = _unitOfWork.Contract
                .GetFirstOrDefault(m => m.ContractId == id);
            if (vendorContracts == null)
            {
                return NotFound();
            }

            return View(vendorContracts);
        }

        // POST: VendorContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vendorContracts = _unitOfWork.Contract.GetFirstOrDefault(u=>u.ContractId==id);
            _unitOfWork.Contract.Remove(vendorContracts);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorContractsExists(int id)
        {
            var contr = _unitOfWork.Contract.GetFirstOrDefault(u => u.ContractId == id);
            if (contr != null)
            {
                return true;
            }
            return false;
        }
    }
}
