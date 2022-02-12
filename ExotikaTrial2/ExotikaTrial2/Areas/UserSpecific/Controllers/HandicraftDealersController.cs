using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using ExotikaTrial2.Models.ViewModels;
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExotikaTrial2.Areas.UserSpecific.Controllers
{
    [Area("UserSpecific"), Authorize(Roles = SD.Role_User_HandicraftDealer)]
    public class HandicraftDealersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandicraftDealersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult HandicraftDealerDashboard()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var statistics = new UserStatsVM()
            {
                visitors = _unitOfWork.Product.GetAll(u => u.HandicraftDealerId == claim.Value).Count(),
                registrations = _unitOfWork.Feedback.GetAll(u=>u.forId==claim.Value).Count()
            };
            return View(statistics);
        }


        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            product.lastUpdated = DateTime.Now.Date;
            product.HandicraftDealerId = claim.Value;
            product.HandicraftDealer = _unitOfWork.HandicraftDealer.GetFirstOrDefault(u => u.HandicraftDealerId == claim.Value);
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product added successfully";
            return RedirectToAction(nameof(ProductsList));
        }

        public IActionResult ProductsList()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var pdtList = _unitOfWork.Product.GetAll(u => u.HandicraftDealerId == claim.Value, includeProperties:"HandicraftDealer");
            return View(pdtList);
        }


        public IActionResult ProductDetails(int? pdtId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (pdtId == null)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == pdtId && u.HandicraftDealerId == claim.Value);
            if (product == null)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            return View(product);
        }


        public IActionResult EditProduct(int? pdtId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (pdtId == null)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == pdtId && u.HandicraftDealerId == claim.Value);
            if (product == null)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct (Product product)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            product.lastUpdated = DateTime.Now.Date;
            //product.HandicraftDealerId = claim.Value;
            product.HandicraftDealer = _unitOfWork.HandicraftDealer.GetFirstOrDefault(u => u.HandicraftDealerId == claim.Value);
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product updated successfully";
            return RedirectToAction(nameof(ProductsList));

        }


        public IActionResult DeleteProduct(int? pdtId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (pdtId == null)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == pdtId && u.HandicraftDealerId == claim.Value);
            if (product == null)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(Product product)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (product.HandicraftDealerId==claim.Value)
            {
                TempData["Error"] = "An error occured";
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction(nameof(ProductsList));
        }
    }
}
