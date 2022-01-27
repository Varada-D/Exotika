using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using ExotikaTrial2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExotikaTrial2.Areas.UserSpecific.Controllers
{
    [Area("UserSpecific"), Authorize(Roles = SD.Role_User_Resort)]
    public class RequirementsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequirementsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult RequirementsList(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var requirementList = _unitOfWork.Requirement.GetAll(u => u.ResortId == claim.Value, includeProperties: "Resort");
            switch (status)
            {
                case "requirementPosted":
                    requirementList = requirementList.Where(u => u.Status == SD.Requirement_Posted);
                    break;
                case "proposalReceived":
                    requirementList = requirementList.Where(u => u.Status == SD.Requirement_ProposalsReceived);
                    break;
                case "proposalAccepted":
                    requirementList = requirementList.Where(u => u.Status == SD.Proposal_Accepted);
                    break;
                default:
                    break;
            }
            return View(requirementList);
        }

        public IActionResult UpsertRequirement(int? requirementId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Requirement requirement = new();
            if (requirementId == null || requirementId == 0)
            {
                return View(requirement);
            }
            else
            {
                requirement = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == requirementId && u.ResortId == claim.Value, includeProperties: "Resort");
                return View(requirement);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertRequirement(Requirement requirement)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                if (requirement.RequirementId == 0)
                {
                    //requirement.lastUpdated = DateTime.Now;
                    // requirement.ResortId = claim.Value;
                    requirement.Resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == claim.Value);
                    requirement.Status = SD.Requirement_Posted;
                    _unitOfWork.Requirement.Add(requirement);
                    TempData["Success"] = "Requirement Details Posted Successfully!";
                }
                else
                {
                    if (requirement.ResortId == claim.Value)
                    {
                        //requirement.lastUpdated = DateTime.Now;
                        requirement.Resort = _unitOfWork.Resort.GetFirstOrDefault(u => u.ResortId == claim.Value);
                        _unitOfWork.Requirement.Update(requirement);
                        TempData["Success"] = "Requirement Details Updated Successfully!";
                    }
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(RequirementsList));   //return RedirectToAction("Index", "ControllerName"); if we want to go to some other controller
            }
            TempData["Error"] = "An error occured. Please try later.";
            return View(requirement);
        }


        public IActionResult Details(int? requirementId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var requirement = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == requirementId && u.ResortId == claim.Value, includeProperties: "Resort");
            return View(requirement);
        }



        public IActionResult Delete(int? requirementId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (requirementId == null || requirementId == 0)
            {
                return NotFound();
            }
            var requirement = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == requirementId && u.ResortId == claim.Value, includeProperties: "Resort");
            if (requirement == null)
            {
                return NotFound();
            }
            return View(requirement);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? requirementId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var requirement = _unitOfWork.Requirement.GetFirstOrDefault(u => u.RequirementId == requirementId && u.ResortId == claim.Value, includeProperties: "Resort");
            //var contracts = _unitOfWork.Contract.GetAll(u => u.RequirementId == requirementId && u.Requirement.ResortId == claim.Value, includeProperties: "Requirement,Vendor,Requirement.Resort");
            if (requirement != null)
            {
                _unitOfWork.Requirement.Remove(requirement);
                _unitOfWork.Save();
                TempData["Success"] = "Requirement Deleted Successfully";
                return RedirectToAction(nameof(RequirementsList));
            }
            TempData["Error"] = "An error occured. Please try later.";
            return RedirectToAction(nameof(RequirementsList));
        }

        public IActionResult ProposalList(int? requirementId)
        {
            if (requirementId == null)
            {
                TempData["Error"] = "An error occured. Please try later.";
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var proposalList = _unitOfWork.Contract.GetAll(u => u.RequirementId == requirementId && u.Requirement.ResortId == claim.Value, includeProperties: "Requirement,Vendor,Requirement.Resort");
            if (proposalList == null)
            {
                TempData["Error"] = "An error occured. Please try later.";
                return NotFound();
            }
            return View(proposalList);
        }


        public IActionResult ProposalDetails(int? contractId)
        {
            if (contractId == null)
            {
                TempData["Error"] = "An error occured. Please try later.";
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var proposal = _unitOfWork.Contract.GetFirstOrDefault(u => u.ContractId == contractId && u.Requirement.ResortId == claim.Value, includeProperties: "Requirement,Vendor,Requirement.Resort");
            if (proposal == null)
            {
                TempData["Error"] = "An error occured. Please try later.";
                return NotFound();
            }
            return View(proposal);
        }

        public IActionResult AcceptProposal(int? contractId)
        {
            if (contractId == null)
            {
                TempData["Error"] = "An error occured. Please try later.";
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var proposal = _unitOfWork.Contract.GetFirstOrDefault(u => u.ContractId == contractId && u.Requirement.ResortId == claim.Value, includeProperties: "Requirement,Vendor,Requirement.Resort");
            if (proposal == null)
            {
                TempData["Error"] = "An error occured. Please try later.";
                return NotFound();
            }
            proposal.Requirement.Status = SD.Proposal_Accepted;
            proposal.Status = SD.Proposal_Accepted;
            _unitOfWork.Contract.Update(proposal);
            _unitOfWork.Save();
            TempData["Success"] = "Proposal Accepted!";
            return RedirectToAction(nameof(ProposalDetails), new { contractId = proposal.ContractId });
        }


        public IActionResult OngoingContracts(string type)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var date = DateTime.Now;
            var ongoingContracts = _unitOfWork.Contract.GetAll(u => u.Requirement.ResortId == claim.Value && u.Status == SD.Proposal_Accepted, includeProperties: "Requirement,Vendor,Requirement.Resort");
            switch (type)
            {
                case "products":
                    ongoingContracts = ongoingContracts.Where(u => u.Requirement.Type == "Product");
                    break;
                case "services":
                    ongoingContracts = ongoingContracts.Where(u => u.Requirement.Type == "Service");
                    break;
                default:
                    break;
            }
            return View(ongoingContracts);
        }
    }
}
