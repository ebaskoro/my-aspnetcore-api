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

    public class HeroesControllerTest : IDisposable
    {

        private readonly SqliteConnection _connection;


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


        HeroesController Target
        {
            get;
        }


        HeroContext Context
        {
            get;
        }


        public void Dispose()
        {
            _connection.Close();
            Context.Dispose();
        }


        [Fact]
        public void Constructor_Initialises_Correctly()
        {
            Assert.Equal(4, Context.Heroes.Count());
            Assert.Equal(1, Context.Heroes.First().Id);
            Assert.Equal("Dare Devil", Context.Heroes.First().Name);
            Assert.Equal(4, Context.Heroes.Last().Id);
            Assert.Equal("Iron Fist", Context.Heroes.Last().Name);
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
        
    }

}
