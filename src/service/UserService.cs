using accesa.src.domain;
using accesa.src.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.service;

public class ServiceUser : IUserService
{
    public IUserRepo userRepository;

    public ServiceUser(IUserRepo userRepository)
    {
        this.userRepository = userRepository;

    }

    public User FindById(long id)
    {
        return userRepository.FindById(id);
    }

    public IEnumerable<User> FindAll()
    {
        return userRepository.FindAll();
    }

    public void Save(User newEntity)
    {

        userRepository.Save(newEntity);
    }

    public void Delete(long id)
    {
        userRepository.Delete(id);
    }

}