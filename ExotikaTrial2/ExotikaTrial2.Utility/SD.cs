using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.Utility
{
    public static class SD
    {
        public const string Role_User_Tourist = "Tourist";
        public const string Role_User_Resort = "Resort";
        public const string Role_User_Vendor = "Vendor";
        public const string Role_User_HandicraftDealer = "HandicraftDealer";
        public const string Role_Admin = "Admin";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        public const string Requirement_Posted = "Posted";
        public const string Requirement_ProposalsReceived = "Proposal(s) Received";

        public const string Proposal_Given = "Proposal Given";
        public const string Proposal_Accepted = "Proposal Accepted";
        public const string Contract_Completed = "Contract Completed";
    }
}
