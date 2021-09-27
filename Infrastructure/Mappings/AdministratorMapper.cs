using Domain.Models.Administrator;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public static class AdministratorMapper
    {

        public static Administrator? MapToAdministrator(AdministratorRecord? administratorRecord)
        {
            if (administratorRecord == null)
            {
                return null;
            }

            return new Administrator(administratorRecord.Email);
        }

        public static AdministratorRecord? MapToAdministratorRecord(Administrator? administrator)
        {
            if (administrator == null)
            {
                return null;
            }

            return new AdministratorRecord
            {
                Email = administrator.Email
            };
        }
    }
}
