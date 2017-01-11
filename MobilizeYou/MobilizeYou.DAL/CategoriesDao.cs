using System.Collections.Generic;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class CategoriesDao : IDao<Category>
    {
        public List<Category> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.Categories.ToList()
                            select new Category
                            {
                                Id = s.Id,
                                Name = s.Name
                            };
                return query.ToList();
            }
        }

        public Category GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                return db.Categories.Find(id);
            }
        }

        public void Add(Category obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Categories.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Category obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Categories.Remove(obj);
                db.SaveChanges();
            }
        }

    }
}
