using ExotikaTrial2.Data;
using ExotikaTrial2.DataAccess.Repository.IRepository;
using ExotikaTrial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.DataAccess.Repository
{
    public class ResortBookingRepository : Repository<ResortBookings>, IResortBookingRepository
    {
        private ExotikaTrial2Context _db;

        public ResortBookingRepository(ExotikaTrial2Context db): base(db)
        {
            _db = db;
        }
        public void Update(ResortBookings obj)
        {
            _db.ResortBookings.Update(obj);
        }
        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _db.ResortBookings.FirstOrDefault(u => u.BookingID == id);
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentIntentId;
        }
    }
}
