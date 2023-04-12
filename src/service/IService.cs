using accesa.src.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.service
{
    public interface IService<TId, Entity> where Entity : Entity<TId>
    {
        Entity FindById(TId id);

        IEnumerable<Entity> FindAll();

        void Save(Entity newEntity);

        void Delete(TId id);

        
    }
}
