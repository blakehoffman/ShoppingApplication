using Dapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Mappings;
using Infrastructure.Records;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdministratorRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Administrator administrator)
        {
            var storedProc = "InsertAdministrator";
            var administratorRecord = AdministratorMapper.MapToAdministratorRecord(administrator);

            _unitOfWork.Connection.Execute(
                storedProc,
                administratorRecord,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);
        }

        public Administrator Find(string email)
        {
            var storedProc = "FindAdministrator";
            AdministratorRecord adminstratorRecord;

            adminstratorRecord = _unitOfWork.Connection.Query<AdministratorRecord>(
                storedProc,
                new { email },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return AdministratorMapper.MapToAdministrator(adminstratorRecord);
        }

        public List<Administrator> GetAll()
        {
            var storedProc = "GetAdministrators";
            List<AdministratorRecord> administratorRecords;

            administratorRecords = _unitOfWork.Connection.Query<AdministratorRecord>(
                storedProc,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            var administrators = new List<Administrator>();

            foreach (var administratorRecord in administratorRecords)
            {
                administrators.Add(new Administrator(administratorRecord.Email));
            }

            return administrators;
        }
    }
}
