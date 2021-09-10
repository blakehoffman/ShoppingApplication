using Domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAdministratorRepository
    {
        public void Add(Administrator administrator);
        public Administrator? Find(string email);
        public List<Administrator> GetAll();
    }
}
