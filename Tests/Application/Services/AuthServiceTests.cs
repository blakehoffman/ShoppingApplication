using Application.Configuration;
using Application.DTO;
using Application.DTO.Authentication;
using Application.Services;
using AutoMapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using Infrastructure.Records;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAdministratorRepository> _administratorRepositoryMock;
        private readonly Mock<AppSettings> _appSettingsMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<IRoleStore<IdentityRole>> _roleStore;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreMock;

        public AuthServiceTests()
        {
            _administratorRepositoryMock = new Mock<IAdministratorRepository>();
            _appSettingsMock = new Mock<AppSettings>();
            _mapperMock = new Mock<IMapper>();
            _roleStore = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(_roleStore.Object, null, null, null, null);
            _userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async void Register()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = "Blake",
                Lastname = "Hoffman",
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO { Succeeded = true };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async void Register_FirstnameGreaterThan100Characters_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = new string('a', 101),
                Lastname = "Hoffman",
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Firstname cannot be empty and cannot be more than 100 characters" }
            };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async void Register_EmptyFirstname_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = "",
                Lastname = "Hoffman",
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO {
                Succeeded = false,
                Errors = { "Firstname cannot be empty and cannot be more than 100 characters" }
            };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async void Register_NullFirstname_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = null,
                Lastname = "Hoffman",
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Firstname cannot be empty and cannot be more than 100 characters" }
            };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async void Register_EmptyLastname_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = "Blake",
                Lastname = "",
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Lastname cannot be empty and cannot be more than 100 characters" }
            };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async void Register_NullLastname_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = "Blake",
                Lastname = null,
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Lastname cannot be empty and cannot be more than 100 characters" }
            };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async void Register_LastnameGreaterThan100Characters_Error()
        {
            _administratorRepositoryMock.Setup(administratorRepository => administratorRepository.Find(It.IsAny<string>()))
                .ReturnsAsync((Administrator)null);

            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddClaimsAsync(It.IsAny<ApplicationUser>(), It.IsAny<List<Claim>>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var authService = new AuthService(_administratorRepositoryMock.Object, _appSettingsMock.Object, _mapperMock.Object, _roleManagerMock.Object, _userManagerMock.Object);

            var registerDTO = new RegisterDTO
            {
                Firstname = "Blake",
                Lastname = new string('a', 101),
                Username = "bhoffman",
                Email = "test@gmail.com",
                Password = "Password1!"
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Lastname cannot be empty and cannot be more than 100 characters" }
            };

            var actual = await authService.Register(registerDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
