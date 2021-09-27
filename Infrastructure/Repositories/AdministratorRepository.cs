using AutoMapper;
using Dapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using Infrastructure.Mappings;
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

        public AdministratorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Administrator administrator)
        {
            var storedProc = "InsertAdministrator";
            var administratorRecord = AdministratorMapper.MapToAdministratorRecord(administrator);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    storedProc,
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
                adminstratorRecord = connection.Query<AdministratorRecord?>(
                    storedProc,
                    new { email },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return AdministratorMapper.MapToAdministrator(adminstratorRecord);
        }

        public List<Administrator> GetAll()
        {
            var storedProc = "GetAdministrators";
            List<AdministratorRecord> administratorRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                administratorRecords = connection.Query<AdministratorRecord>(
                    storedProc,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            var administrators = new List<Administrator>();

            foreach (var administratorRecord in administratorRecords)
            {
                administrators.Add(new Administrator(administratorRecord.Email));
            }

            return administrators;
        }
    }
}
