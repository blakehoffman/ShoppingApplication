using Application.DTO;
using Application.DTO.Discount;
using Application.Services;
using AutoMapper;
using Domain.Models.Discount;
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
    public class DiscountServiceTests
    {
        private readonly Mock<IDiscountRepository> _discountRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public DiscountServiceTests()
        {
            _discountRepositoryMock = new Mock<IDiscountRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void CreateDiscount()
        {
            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = "test",
                Amount = 0.25,
            };

            var expected = new ResultDTO
            {
                Errors = { },
                Succeeded = true
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateDiscount_AmountEqualTo0_Error()
        {
            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = "test",
                Amount = 0,
            };

            var expected = new ResultDTO
            {
                Errors = { "Discount amount cannot be less than or equal to 0" },
                Succeeded = false
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateDiscount_AmountLessThan0_Error()
        {
            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = "test",
                Amount = -1,
            };

            var expected = new ResultDTO
            {
                Errors = { "Discount amount cannot be less than or equal to 0" },
                Succeeded = false
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateDiscount_CodeGreaterThan50Characters_Error()
        {
            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = new string('a', 51),
                Amount = 0.25,
            };

            var expected = new ResultDTO
            {
                Errors = { "Discount code cannot be empty and must be between 4 and 50 characters" },
                Succeeded = false
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateDiscount_CodeLessThan4Characters_Error()
        {
            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = new string("tes"),
                Amount = 0.25,
            };

            var expected = new ResultDTO
            {
                Errors = { "Discount code cannot be empty and must be between 4 and 50 characters" },
                Succeeded = false
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateDiscount_DuplicateCode_Error()
        {
            _discountRepositoryMock.Setup(discountRepository => discountRepository.FindByCode(It.IsAny<string>()))
                .ReturnsAsync(new Discount(Guid.NewGuid(), "Test code", 0.25));


            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = "Test code",
                Amount = 0.25,
                Active = true
            };

            var expected = new ResultDTO
            {
                Errors = { "A discount with this code already exists" },
                Succeeded = false
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateDiscount_EmptyCode_Error()
        {
            var discountService = new DiscountService(_discountRepositoryMock.Object, _mapperMock.Object);

            var createDiscountDTO = new CreateDiscountDTO
            {
                Code = "",
                Amount = 0.25,
            };

            var expected = new ResultDTO
            {
                Errors = { "Discount code cannot be empty and must be between 4 and 50 characters" },
                Succeeded = false
            };

            var actual = discountService.CreateDiscount(createDiscountDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
