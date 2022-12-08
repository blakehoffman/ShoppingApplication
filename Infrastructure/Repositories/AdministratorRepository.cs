using Domain.Models.Administrator;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public AdministratorRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Administrator administrator)
        {
            var administratorEntity = AdministratorMapper.MapToAdministratorEntity(administrator);
            _applicationDbContext.Administrators.Add(administratorEntity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Administrator> Find(string email)
        {
            var administratorEntity = await _applicationDbContext.Administrators
                .AsNoTracking()
                .FirstOrDefaultAsync(administrator => administrator.Email == email);

            return AdministratorMapper.MapToAdministrator(administratorEntity);
        }

        public async Task<List<Administrator>> GetAll()
        {
            var administratorEntities = await _applicationDbContext.Administrators
                .AsNoTracking()
                .ToListAsync();

            var administrators = new List<Administrator>();

            foreach (var administratorEntity in administratorEntities)
            {
                administrators.Add(AdministratorMapper.MapToAdministrator(administratorEntity));
            }

            return administrators;
        }
    }
}
