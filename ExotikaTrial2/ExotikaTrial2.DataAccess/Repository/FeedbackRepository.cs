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
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private ExotikaTrial2Context _db;

        public FeedbackRepository(ExotikaTrial2Context db): base(db)
        {
            _db = db;
        }
        public void Update(Feedback obj)
        {
            _db.Feedbacks.Update(obj);
        }
    }
}
