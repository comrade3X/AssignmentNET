using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class MembershipDao : IDao<Membership>
    {
        public List<Membership> GetAll()
        {
            using (MobilizeYouEntities db = new MobilizeYouEntities())
            {
                var query = from s in db.Memberships.ToList()
                            select new Membership
                            {
                                Id = s.Id,
                                Employee = s.Employee,
                                Role = s.Role,
                                Username = s.Username,
                                Password = s.Password,
                                EmployeeId = s.EmployeeId,
                                RoleId = s.RoleId
                            };
                return query.ToList();
            }
        }

        public Membership GetById(int id)
        {
            using (MobilizeYouEntities db = new MobilizeYouEntities())
            {
                var s = db.Memberships.Find(id);
                var memberShip = new Membership
                {
                    Id = s.Id,
                    Employee = s.Employee,
                    Role = s.Role,
                    Username = s.Username,
                    Password = s.Password
                };
                return memberShip;
            }
        }

        public void Add(Membership obj)
        {
            using (MobilizeYouEntities db = new MobilizeYouEntities())
            {
                db.Memberships.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Membership obj)
        {
            using (MobilizeYouEntities db = new MobilizeYouEntities())
            {
                db.Memberships.Attach(obj);
                db.Memberships.Remove(obj);
                db.SaveChanges();
            }
        }

        public void Update(Membership obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("");
            }

            using (var db = new MobilizeYouEntities())
            {
                //db.Entry(obj).State = EntityState.Modified;
                db.Set<Membership>().AddOrUpdate(obj);
                // other changed properties
                db.SaveChanges();
            }
        }

        public Membership Login(string userName, string passWord)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.Memberships.FirstOrDefault(x => userName.Equals(x.Username) && passWord.Equals(x.Password));
                if (s == null) return null;

                var res = new Membership
                {
                    Id = s.Id,
                    Username = s.Username,
                    Password = s.Password,
                    EmployeeId = s.EmployeeId,
                    Employee = s.Employee,
                    Role = s.Role,
                    RoleId = s.RoleId
                };
                return res;
            }
        }
    }
}
