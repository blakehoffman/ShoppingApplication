using Domain.Models.Administrator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAdministratorRepository
    {
        public Task Add(Administrator administrator);
        public Task<Administrator> Find(string email);
        public Task<List<Administrator>> GetAll();
    }
}
