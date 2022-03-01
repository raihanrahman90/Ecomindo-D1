using AutoMapper;
using Ecomindo_D1.Model;
using Ecomindo_D1.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;
using MockQueryable.Moq;
using Moq;
using Ecomindo_D1.bll.test.Common;
using Ecomindo_D1.Interface;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Ecomindo_D1.DTO;
using FluentAssertions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ecomindo_D1.bll.test
{
    public class RestaurantTest
    {
        private IEnumerable<Restaurant> restaurant;
        private IEnumerable<RestaurantWithMenusDTO> restaurantWithMenus;
        private IEnumerable<Menu> menus;
        private Mapper mapper;
        private Mock<ILogger<RestaurantService>> logger;
        private Mock<IRedisService> redis;
        private Mock<IUnitOfWork> uow;
        public RestaurantTest()
        {
            restaurant = CommonHelper.LoadDataFromFile<IEnumerable<Restaurant>>(@"MockData\Restaurant.json");
            menus = CommonHelper.LoadDataFromFile<IEnumerable<Menu>>(@"MockData\Menu.json");
            restaurantWithMenus = CommonHelper.LoadDataFromFile<IEnumerable<RestaurantWithMenusDTO>>(@"MockData\RestaurantWithMenu.json");
            uow = MockUnitOfWork();
            redis = MockRedis();
            logger = MockLogger();
            mapper = InitMapper();
        }

        private RestaurantService CreateRestaurantService()
        {
            return new RestaurantService(logger.Object, uow.Object, mapper, redis.Object);
        }
        #region method mock depedencies

        private Mapper InitMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfileConfiguration>();
            }));
        }
        private Mock<IUnitOfWork> MockUnitOfWork()
        {
            Mock<IRestaurantRepository> mockRestaurantRepo = MockRestaurantRepo();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.RestaurantRepository).Returns(mockRestaurantRepo.Object);
            mockUnitOfWork.Setup(x => x.SaveAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
            return mockUnitOfWork;
        }
        private Mock<IRestaurantRepository> MockRestaurantRepo()
        {
            var mockRestaurantRepo = new Mock<IRestaurantRepository>();

            foreach (var resto in restaurant)
            {
                resto.Menus = menus.Where(x => x.idMenu == resto.idRestaurant).ToList();
            }
            IQueryable<Restaurant> restaurantsQueryable = restaurant.AsQueryable();

            mockRestaurantRepo
                .Setup(u => u.GetAll())
                .Returns(restaurantsQueryable.BuildMock().Object);

            mockRestaurantRepo
                .Setup(u => u.IsExist(It.IsAny<Expression<Func<Restaurant, bool>>>()))
                .Returns((Expression<Func<Restaurant, bool>> condition) => restaurantsQueryable.Any(condition));

            mockRestaurantRepo
                .Setup(u => u.GetSingleAsync(It.IsAny<Expression<Func<Restaurant, bool>>>()))
                .ReturnsAsync((Expression<Func<Restaurant, bool>> condition) => restaurantsQueryable.FirstOrDefault(condition));
            mockRestaurantRepo
                .Setup(u => u.AddAsync(It.IsAny<Restaurant>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Restaurant restaurant, CancellationToken token) =>
                {
                    restaurant.idRestaurant = Guid.NewGuid();
                    return restaurant;
                });

            mockRestaurantRepo
                .Setup(u => u.Delete(It.IsAny<Expression<Func<Restaurant, bool>>>()));

            return mockRestaurantRepo;
        }
        private Mock<ILogger<RestaurantService>> MockLogger()
        {
            var mockLogger = new Mock<ILogger<RestaurantService>>();
            return mockLogger;
        }
        private Mock<IRedisService> MockRedis()
        {
            var mockRedis = new Mock<IRedisService>();

            mockRedis
                .Setup(x => x.GetAsync<RestaurantWithMenusDTO>(It.Is<string>(x => x.Equals("restaurant_restaurantID:311b848d-00ae-443e-5e85-08d946c2256c"))))
                .ReturnsAsync(restaurantWithMenus.FirstOrDefault(x => x.idRestaurant == Guid.Parse("311b848d-00ae-443e-5e85-08d946c2256c")))
                .Verifiable();

            mockRedis
                .Setup(x => x.SaveAsync(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockRedis
                .Setup(x => x.DeleteAsync(It.IsAny<string>())).Verifiable();

            return mockRedis;

        }

        #endregion method mock depedencies

        [Fact]
        public async Task GetAllAsync_Success()
        {
            var expected = restaurant;

            var svc = CreateRestaurantService();

            var actual = await svc.getAll();

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("311b848d-00ae-443e-5e85-08d946c2256c")]
        public async Task GetByRestaurantId_Success(string restaurantID)
        {
            //arange
            var id = Guid.Parse(restaurantID);
            var expected = restaurantWithMenus.First(x => x.idRestaurant == id);
            var svc = CreateRestaurantService();
            //act
            var actual = await svc.GetOne(id);
            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("311b848d-00ae-443e-5e85-08d946c2256c")]
        public async Task GetByRestaurantId_InRedis_Success(string restaurantID)
        {
            var id = Guid.Parse(restaurantID);
            var expected = restaurantWithMenus.First(x => x.idRestaurant == id);
            var svc = CreateRestaurantService();
            var actual = await svc.GetOne(id);

            actual.Should().BeEquivalentTo(expected);

            redis.Verify(x => x.GetAsync<RestaurantWithMenusDTO>($"restaurant_restaurantID:{restaurantID}"), Times.Once);
            redis.Verify(x => x.SaveAsync($"restaurant_restaurantID:{restaurantID}", It.IsAny<RestaurantWithMenusDTO>()), Times.Never());
        }

        [Theory]
        [InlineData("311b848d-00ae-443e-5e85-08d946c2256a")]
        public async Task GetByRestaurantId_NotInRedis_Success(string restaurantID)
        {
            var id = Guid.Parse(restaurantID);
            var expected = restaurantWithMenus.First(x => x.idRestaurant == id);
            var svc = CreateRestaurantService();

            var actual = await svc.GetOne(id);

            actual.Should().BeEquivalentTo(expected);

            redis.Verify(x => x.GetAsync<RestaurantWithMenusDTO>($"restaurant_restaurantID:{restaurantID}"), Times.Once);
            redis.Verify(x => x.SaveAsync($"restaurant_restaurantID:{restaurantID}", It.IsAny<RestaurantWithMenusDTO>()), Times.Once);

        }

        [Fact]
        public async Task CreateRestaurant_Success()
        {
            var expected = new RestaurantDTO
            {
                namaRestaurant = "test restaurant"
            };
            var svc = CreateRestaurantService();
            Func<Task> act = async () => { await svc.insert(expected); };
            await act.Should().NotThrowAsync<Exception>();
            uow.Verify(x => x.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }



    }
}
