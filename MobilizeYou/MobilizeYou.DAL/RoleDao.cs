using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MobilizeYou.DAL
{
    using DTO;
    public class RoleDao : IDao<Role>
    {
        public List<Role> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.Roles.ToList()
                            select new Role
                            {
                                Id = s.Id,
                                Name = s.Name,
                            };
                return query.ToList();
            }
        }

        public Role GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.Roles.Find(id);
                var role = new Role
                {
                    Id = s.Id,
                    Name = s.Name
                };
                return role;
            }
        }

        public void Add(Role obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Roles.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Role obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Roles.Attach(obj);
                db.Roles.Remove(obj);
                db.SaveChanges();
            }
        }

        public void Update(Role obj)
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
