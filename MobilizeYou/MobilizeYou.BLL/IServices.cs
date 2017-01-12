using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilizeYou.BLL
{
    interface IServices<TE>
    {
        List<TE> GetAll();

        TE GetById(int id);

        void Add(TE obj);

        void Delete(TE obj);

        void Update(TE obj);
    }
}
