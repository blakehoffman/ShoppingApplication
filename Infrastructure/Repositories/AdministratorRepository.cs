using AutoMapper;
using Dapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public AdministratorRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void Add(Administrator administrator)
        {
            var storedProc = "InsertAdministrator";
            var administratorRecord = _mapper.Map<AdministratorRecord>(administrator);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc,
                    administratorRecord,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Administrator? Find(string email)
        {
            var storedProc = "FindAdministrator";
            AdministratorRecord? adminstratorRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                adminstratorRecord = connection.Query<AdministratorRecord?>(storedProc,
                    new { email },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return _mapper.Map<Administrator?>(adminstratorRecord);
        }

        public List<Administrator> GetAll()
        {
            var storedProc = "GetAdministrators";
            List<AdministratorRecord> administratorRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                administratorRecords = connection.Query<AdministratorRecord>(storedProc,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            return _mapper.Map<List<Administrator>>(administratorRecords);
        }
    }
}
