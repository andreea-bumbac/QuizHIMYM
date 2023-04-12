using accesa.src.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.repo
{
    public interface IUserRepo : IRepository<long, User>
    {
        User GetByUsername(string username);

        void Update(string username, string password, int newTokens, int newintr);
    }
}
