using System.Collections.Generic;

namespace MobilizeYou.DAL
{
    public interface IDao<TE>
    {
        List<TE> GetAll();

        TE GetById(int id);

        void Add(TE obj);

        void Delete(TE obj);
    }
}
