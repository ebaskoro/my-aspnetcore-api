using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Api.Controllers;
using Api.Models;


namespace Api.Tests.Controllers
{

    /// <summary>
    /// Unit tests for the Heroes controller.
    /// </summary>
    public class HeroesControllerTest : IDisposable
    {

        private readonly SqliteConnection _connection;


        /// <summary>
        /// Creates the test contexts.
        /// </summary>
        public HeroesControllerTest()
        {
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = ":memory:"
            };
            var connectionString = builder.ToString();
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<HeroContext>()
                .UseSqlite(_connection)
                .Options;
            Context = new HeroContext(options);
            Context.Database.EnsureCreated();

            Target = new HeroesController(Context);
        }


        /// <summary>
        /// Gets the target to test.
        /// </summary>
        HeroesController Target
        {
            get;
        }


        /// <summary>
        /// Gets the database context.
        /// </summary>
        HeroContext Context
        {
            get;
        }


        /// <summary>
        /// Disposes the test context.
        /// </summary>
        public void Dispose()
        {
            _connection.Close();
            Context.Dispose();
        }


        [Fact]
        public void Constructor_Initialises_Correctly()
        {
            Assert.Equal(5, Context.Heroes.Count());
            Assert.Equal(1, Context.Heroes.First().Id);
            Assert.Equal("Dare Devil", Context.Heroes.First().Name);
            Assert.Equal(5, Context.Heroes.Last().Id);
            Assert.Equal("Arrow", Context.Heroes.Last().Name);
        }


        [Fact]
        public async void GetAll_Returns_NotNull()
        {
            var actual = await Target.GetAll();

            Assert.NotNull(actual);
        }


        [Fact]
        public async void GetAll_Returns_InstanceOf_OkObjectResult()
        {
            var actual = await Target.GetAll();

            Assert.IsType<OkObjectResult>(actual);
        }


        [Fact]
        public async void GetAll_Returns_Value_InstanceOf_ListOfHeroes()
        {
            var actionResult = await Target.GetAll();
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = okObjectResult.Value;

            Assert.IsType<List<Hero>>(actual);
        }


        [Fact]
        public async void GetById_Returns_NotNull()
        {
            var actual = await Target.GetById(0);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void GetById_When_InexistentId_Returns_InstanceOf_NotFoundResult()
        {
            var actual = await Target.GetById(999);

            Assert.IsType<NotFoundResult>(actual);
        }


        [Fact]
        public async void GetById_When_ExistentId_Returns_InstanceOf_OkObjecResult()
        {
            var actual = await Target.GetById(1);

            Assert.IsType<OkObjectResult>(actual);
        }


        [Fact]
        public async void GetById_When_ExistentId_Returns_Value_InstanceOf_Hero()
        {
            var actionResult = await Target.GetById(1);
            var okObjectResult = (OkObjectResult)actionResult;
            var actual = okObjectResult.Value;

            Assert.IsType<Hero>(actual);
        }


        [Fact]
        public async void Create_Returns_NotNull()
        {
            var actual = await Target.Create(null);

            Assert.NotNull(actual);
        }


        [Fact]
        public async void Create_When_NullHero_Returns_InstanceOf_BadRequestResult()
        {
            var actual = await Target.Create(null);

            Assert.IsType<BadRequestResult>(actual);
        }


        [Fact]
        public async void Create_When_NonNullHero_Returns_InstanceOf_CreatedAtRouteResult()
        {
            var actual = await Target.Create(new Hero());

            Assert.IsType<CreatedAtRouteResult>(actual);
        }


        [Fact]
        public async void Create_When_NonNullHero_Returns_Correctly()
        {
            var actionResult = await Target.Create(new Hero());
            var actual = (CreatedAtRouteResult)actionResult;

            Assert.Equal("GetHeroById", actual.RouteName);
            Assert.Equal(6L, actual.RouteValues["id"]);
            Assert.IsType<Hero>(actual.Value);
        }


        [Fact]
        public async void Create_When_NonNullHero_Updates_Database_Correctly()
        {
            await Target.Create(new Hero());
            var actual = Context.Heroes.Count();

            Assert.Equal(6, actual);
        }
        
    }

}
