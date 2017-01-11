using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class ProductServices : IServices<Product>
    {
        readonly ProductDao _productDao = new ProductDao();
        public List<Product> GetAll()
        {
            return _productDao.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDao.GetById(id);
        }

        public void Add(Product obj)
        {
            _productDao.Add(obj);
        }

        public void Delete(Product obj)
        {
            _productDao.Delete(obj);
        }

        public void Update(Product obj)
        {
            _productDao.Update(obj);
        }
    }
}
