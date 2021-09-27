using Application.DTO;
using Application.DTO.Administrator;
using Application.Services;
using AutoMapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void CreateAdministrator()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .Returns((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = "test@gmail.com" };

            var expected = new ResultDTO { Succeeded = true };

            var actual = adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateAdministrator_DuplicateEmail_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .Returns(new Administrator("test@gmail.com"));

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = "test@gmail.com" };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Admin already exists for this email" }
            };

            var actual = adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateAdministrator_EmailGreaterThan100Characters_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .Returns((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = new string('a', 101) };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Email cannot be empty and cannot be greater than 100 characters" }
            };

            var actual = adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateAdministrator_EmptyEmail_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .Returns((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = "" };

            var expected = new ResultDTO {
                Succeeded = false,
                Errors = { "Email cannot be empty and cannot be greater than 100 characters" }
            };

            var actual = adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateAdministrator_NullEmail_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .Returns((Administrator)null);

            var adminService = new AdminService(_administratorRepositoryMock.Object, _mapperMock.Object);
            var createAdministratorDTO = new CreateAdministratorDTO { Email = null };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Email cannot be empty and cannot be greater than 100 characters" }
            };

            var actual = adminService.CreateAdministrator(createAdministratorDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
