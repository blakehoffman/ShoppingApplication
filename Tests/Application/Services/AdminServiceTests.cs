using Application.DTO;
using Application.DTO.Administrator;
using Application.Services;
using AutoMapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Services
{
    public class AdminServiceTests
    {
        private readonly Mock<IAdministratorRepository> _administratorRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public AdminServiceTests()
        {
            _administratorRepositoryMock = new Mock<IAdministratorRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateAdministrator()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = "test@gmail.com" };

            var expected = new ResultDTO { Succeeded = true };

            var actual = await adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateAdministrator_DuplicateEmail_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync(new Administrator("test@gmail.com"));

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = "test@gmail.com" };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Admin already exists for this email" }
            };

            var actual = await adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateAdministrator_EmailGreaterThan100Characters_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = new string('a', 101) };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Email cannot be empty and cannot be greater than 100 characters" }
            };

            var actual = await adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateAdministrator_EmptyEmail_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = "" };

            var expected = new ResultDTO {
                Succeeded = false,
                Errors = { "Email cannot be empty and cannot be greater than 100 characters" }
            };

            var actual = await adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateAdministrator_NullEmail_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = null };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Email cannot be empty and cannot be greater than 100 characters" }
            };

            var actual = await adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
