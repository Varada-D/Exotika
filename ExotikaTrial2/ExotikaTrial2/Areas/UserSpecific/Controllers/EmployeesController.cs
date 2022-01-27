#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExotikaTrial2.Data;
using Microsoft.AspNetCore.Authorization;
using ExotikaTrial2.Utility;
using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using System.Security.Claims;

namespace ExotikaTrial2.Controllers
{
    [Area("UserSpecific")]
    [Authorize(Roles = SD.Role_User_Resort)]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var employeesList = _unitOfWork.Employee.GetAll(u => u.ResortId == claim.Value);
            return View(employeesList);
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var employee = _unitOfWork.Employee
                .GetFirstOrDefault(m => m.EmployeeId == id && m.ResortId==claim.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            employee.ResortId = claim.Value;
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(employee);
                _unitOfWork.Save();
                TempData["Success"] = "Emmployee Added Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Employee Addition Failed";
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var employee = _unitOfWork.Employee.GetFirstOrDefault(u=>u.EmployeeId==id && u.ResortId==claim.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                TempData["Error"] = "Error Updating Employee Details";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Employee.Update(employee);
                    _unitOfWork.Save();
                    TempData["Success"] = "Employee Details Updated Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["Error"] = "Error Updating Employee Details";
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var employee = _unitOfWork.Employee
                .GetFirstOrDefault(m => m.EmployeeId == id && m.ResortId == claim.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var employee = _unitOfWork.Employee.GetFirstOrDefault(u=>u.EmployeeId== id && u.ResortId == claim.Value);
            _unitOfWork.Employee.Remove(employee);
            _unitOfWork.Save();
            TempData["Success"] = "Employee Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        
        private bool EmployeeExists(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var emp = _unitOfWork.Employee.GetFirstOrDefault(e => e.EmployeeId == id && e.ResortId == claim.Value);
            if (emp != null)
            {
                return true;
            }
            return false;
        }
    }
}
