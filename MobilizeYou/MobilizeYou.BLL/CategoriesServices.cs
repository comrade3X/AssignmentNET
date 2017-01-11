using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class CategoriesServices : IServices<Category>
    {
        readonly CategoriesDao _categoriesDao = new CategoriesDao();
        public List<Category> GetAll()
        {
            return _categoriesDao.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoriesDao.GetById(id);
        }

        public void Add(Category obj)
        {
            _categoriesDao.Add(obj);
        }

        public void Delete(Category obj)
        {
            _categoriesDao.Delete(obj);
        }
    }
}
