using accesa.src.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.repo
{
    public interface IRepository<TId, Entity> where Entity : Entity<TId>
    {
        Entity FindById(TId id);

        IEnumerable<Entity> FindAll();

        void Save(Entity newEntity);

        void Delete(TId id);

    }
}
