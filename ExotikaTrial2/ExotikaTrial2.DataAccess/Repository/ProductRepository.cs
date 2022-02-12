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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ExotikaTrial2Context _db;

        public ProductRepository (ExotikaTrial2Context db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
