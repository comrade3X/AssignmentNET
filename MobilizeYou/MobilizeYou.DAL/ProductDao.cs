using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class ProductDao : IDao<Product>
    {
        public List<Product> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.Products.ToList()
                            select new Product()
                            {
                                Id = s.Id,
                                Name = s.Name,
                                YearOfRegistion = s.YearOfRegistion,
                                Make = s.Make,
                                Model = s.Model,
                                AddOns = s.AddOns,
                                RentPerDay = s.RentPerDay,
                                Category = s.Category,
                                CategoryId = s.CategoryId
                            };
                return query.ToList();
            }
        }

        public Product GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.Products.Find(id);
                var product = new Product
                {
                    Id = s.Id,
                    Name = s.Name,
                    YearOfRegistion = s.YearOfRegistion,
                    Make = s.Make,
                    Model = s.Model,
                    AddOns = s.AddOns,
                    RentPerDay = s.RentPerDay,
                    Category = s.Category,
                    CategoryId = s.CategoryId
                };
                return product;
            }
        }

        public void Add(Product obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Products.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Product obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Products.Attach(obj);
                db.Products.Remove(obj);
                db.SaveChanges();
            }
        }

        public void Update(Product obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("");
            }

            using (var db = new MobilizeYouEntities())
            {
                db.Entry(obj).State = EntityState.Modified;
                // other changed properties
                db.SaveChanges();
            }
        }
    }
}
