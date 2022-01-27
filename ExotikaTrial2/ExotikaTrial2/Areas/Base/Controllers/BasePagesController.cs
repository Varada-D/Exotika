using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using ExotikaTrial2.Models.ViewModels;
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace ExotikaTrial2.Controllers
{
    [Area("Base")]
    public class BasePagesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BasePagesController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ResortHome()
        {
            return View();
        }

        public IActionResult HandicraftDealerHome()
        {
            return View();
        }

        public IActionResult ResortDetails(string? resortId)
        {
            if (resortId != null)
            {
                ResortDetailsVM resortData = new ResortDetailsVM
                {
                    resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == resortId),
                    packages = _unitOfWork.Package.GetAll(u => u.ResortId == resortId)
                };

                return View(resortData);
            }
            return NotFound();
        }

        public IActionResult ResortsList()
        {
            var resorts = _unitOfWork.Resort.GetAll();
            return View(resorts);
        }

        public IActionResult VendorHome()
        {
            var contracts = _unitOfWork.Requirement.GetAll(u=>u.Status==SD.Requirement_Posted, includeProperties: "Resort");
            return View(contracts);
        }

        public IActionResult Book(int? packageId)
        {
            if (packageId == null)
            {
                return NotFound();
            }
            var resId = _unitOfWork.Package.GetFirstOrDefault(u => u.PackageId == packageId).ResortId;
            var res = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == resId);
            var pkg = _unitOfWork.Package.GetFirstOrDefault(u => u.PackageId == packageId);
            var pkgBook = new ResortBookings()
            {
                PackageId = packageId,
                ResortId =resId,
                Resort = res,
                Package = pkg
            };
            return View(pkgBook);
        }

        public IActionResult Proposal(int? requirementId)
        {
            if (requirementId == null)
            {
                return NotFound();
            }
            var resId = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == requirementId).ResortId;
            var res = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == resId);
            var req = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == requirementId);
            var contProposal = new Contract()
            {
                RequirementId = (int)requirementId,
                //ResortId = resId,
                //Resort = res,
                Requirement = req
            };
            return View(contProposal);
        }


        [HttpPost]
        [Authorize(Roles = SD.Role_User_Vendor)]
        [ValidateAntiForgeryToken]
        public IActionResult Proposal(Contract newContract, IFormFile file)
        {
            if (!User.IsInRole(SD.Role_User_Vendor))
            {
                TempData["Error"] = "Please Login to your account to continue";
            }
            if (ModelState.IsValid && User.IsInRole(SD.Role_User_Vendor))
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"proposalFiles");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    newContract.ProposalFile = @"\proposalFiles\" + filename + extension;
                }
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                newContract.VendorId = claim.Value;
                newContract.Vendor = _unitOfWork.Vendor.GetFirstOrDefault(u => u.VendorId == claim.Value);
                newContract.Requirement = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == newContract.RequirementId);
                //newContract.ResortId = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == newContract.RequirementId).ResortId;
                //newContract.Resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == newContract.ResortId);
                newContract.CreateDate = DateTime.Now;
                newContract.Status = SD.Proposal_Given;
                newContract.Requirement.Status = SD.Requirement_ProposalsReceived;
                _unitOfWork.Contract.Add(newContract);
                _unitOfWork.Save();
                TempData["Success"] = "Proposal sent successfully";
                return RedirectToAction(nameof(VendorHome));
            }
            TempData["Error"] = "An error occured. Please try later!";
            return View(newContract);
        }


        [HttpPost]
        [ActionName("Book")]
        [Authorize(Roles = SD.Role_User_Tourist)]
        [ValidateAntiForgeryToken]
        public IActionResult Book(ResortBookings newBooking)
        {
            if (!User.IsInRole(SD.Role_User_Tourist))
            {
                TempData["Error"] = "Please Login to your tourist account to continue";
            }
            if (ModelState.IsValid && User.IsInRole(SD.Role_User_Tourist))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                newBooking.TouristId = claim.Value;
                newBooking.TouristDetails = _unitOfWork.Tourist.GetFirstOrDefault(u => u.TouristId == newBooking.TouristId);
                newBooking.Resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == newBooking.ResortId);
                newBooking.Package = _unitOfWork.Package.GetFirstOrDefault(u => u.PackageId == newBooking.PackageId);
                int duration = (newBooking.CheckOutDate.Date - newBooking.CheckInDate.Date).Days;
                newBooking.TotalPrice = (newBooking.Package.Price * newBooking.noOfBookings * duration);
                newBooking.CreateDate = DateTime.Now;
                _unitOfWork.ResortBooking.Add(newBooking);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Book_Summary), new { bookingId = newBooking.BookingID });
            }
            return View(newBooking);
        }


        [Authorize(Roles = SD.Role_User_Tourist)]
        public IActionResult Book_Summary(int? bookingId)
        {

            ResortBookings book = _unitOfWork.ResortBooking.GetFirstOrDefault(u => u.BookingID == bookingId, includeProperties: "Package,Resort,TouristDetails");
            return View(book);
        }


        [HttpPost]
        [ActionName("Book_Summary")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =SD.Role_User_Tourist)]
        public IActionResult Book_Payment(int? bookingId)
        {
            if (!User.IsInRole(SD.Role_User_Tourist))
            {
                TempData["Error"] = "Please Login to your account to continue";
            }
            ResortBookings book = _unitOfWork.ResortBooking.GetFirstOrDefault(u => u.BookingID == bookingId, includeProperties: "Package,Resort,TouristDetails");
            if (ModelState.IsValid && User.IsInRole(SD.Role_User_Tourist)) {
                int duration = (book.CheckOutDate.Date - book.CheckInDate.Date).Days;
                book.TotalPrice = (book.Package.Price * book.noOfBookings * duration);
                book.CreateDate = DateTime.Now;
                _unitOfWork.ResortBooking.Update(book);
            }
            // Stripe Settings
            var domain = "https://localhost:44378/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = book.TotalPrice,
              Currency = "inr",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = book.Package.Name,
              },

            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = domain + $"Base/BasePages/PaymentConfirmation?bookingId={book.BookingID}",
                CancelUrl = domain + $"Base/BasePages/Book_Summary?bookingId={book.BookingID}",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            _unitOfWork.ResortBooking.UpdateStripePaymentId(book.BookingID, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            TempData["Success"] = "Booking Successful";
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }



        [Authorize(Roles =SD.Role_User_Tourist)]
        public IActionResult PaymentConfirmation(int? bookingId)
        {
            ResortBookings book = _unitOfWork.ResortBooking.GetFirstOrDefault(u => u.BookingID == bookingId, includeProperties:"Package,Resort,TouristDetails");
            return View(book);
        }
    }
}
