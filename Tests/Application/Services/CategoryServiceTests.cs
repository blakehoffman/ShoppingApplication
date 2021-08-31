using Application.DTO;
using Application.DTO.Category;
using Application.Services;
using AutoMapper;
using Domain.Models.Categories;
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
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void CreateCategory_DuplicateName_Error()
        {
            _categoryRepositoryMock.Setup(categoryRepository => categoryRepository.FindByName(It.IsAny<string>()))
                .Returns(new Category(Guid.NewGuid(), "Test Name", null));


            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            var createCategoryDTO = new CreateCategoryDTO
            {
                Name = "Test name",
                ParentId = null
            };

            var expected = new ResultDTO
            {
                Errors = { "A category with this name already exists" },
                IsSuccess = false
            };

            var actual = categoryService.CreateCategory(createCategoryDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCategory_EmptyName_Error()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            var createCategoryDTO = new CreateCategoryDTO
            {
                Name = "",
                ParentId = null
            };

            var expected = new ResultDTO
            {
                Errors = { "Category name cannot be empty and between 3 and 100 characters" },
                IsSuccess = false
            };

            var actual = categoryService.CreateCategory(createCategoryDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCategory_InvalidParent_Error()
        {
            _categoryRepositoryMock.Setup(categoryRepository => categoryRepository.Find(It.IsAny<Guid>()))
                .Returns((Category)null);

            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            var createCategoryDTO = new CreateCategoryDTO
            {
                Name = "Test name",
                ParentId = Guid.NewGuid()
            };

            var expected = new ResultDTO
            {
                Errors = { "Parent category doesn't exist" },
                IsSuccess = false
            };

            var actual = categoryService.CreateCategory(createCategoryDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCategory_NameGreaterThan100Chracters_Error()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);
            var createCategoryDTO = new CreateCategoryDTO
            {
                Name = new string('a', 101),
                ParentId = null
            };

            var expected = new ResultDTO
            {
                Errors = { "Category name cannot be empty and between 3 and 100 characters" },
                IsSuccess = false
            };

            var actual = categoryService.CreateCategory(createCategoryDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCategory_NameLessThan3Characters_Error()
        {
            var categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapperMock.Object);

            var createCategoryDTO = new CreateCategoryDTO
            {
                Name = "te",
                ParentId = null
            };

            var expected = new ResultDTO
            {
                Errors = { "Category name cannot be empty and between 3 and 100 characters" },
                IsSuccess = false
            };

            var actual = categoryService.CreateCategory(createCategoryDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
