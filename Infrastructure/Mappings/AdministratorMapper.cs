using Domain.Models.Administrator;
using EntityModels = Infrastructure.Models;

namespace Infrastructure.Mappings
{
    public static class AdministratorMapper
    {
        public static Administrator MapToAdministrator(EntityModels.Administrator administratorEntity)
        {
            if (administratorEntity == null)
            {
                return null;
            }

            return new Administrator(administratorEntity.Email);
        }

        public static EntityModels.Administrator MapToAdministratorEntity(Administrator administrator)
        {
            if (administrator == null)
            {
                return null;
            }

            return new EntityModels.Administrator
            {
                Email = administrator.Email
            };
        }
    }
}
