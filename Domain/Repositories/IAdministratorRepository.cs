using Domain.Models.Administrator;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IAdministratorRepository
    {
        public void Add(Administrator administrator);
        public Administrator Find(string email);
        public List<Administrator> GetAll();
    }
}
