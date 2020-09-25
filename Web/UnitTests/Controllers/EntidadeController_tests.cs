using AppCore.Dtos;
using AppCore.Implementations;
using AppCore.Models;
using AppCore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class EntidadeController_tests
    {
        Mock<IEF_Repository<Entidade>> _stub_Repository;
        Mock<EF_Service<Entidade>> _stub_Service;
        Mock<EntidadeController> _mock_controller;

        public EntidadeController_tests()
        {
            _stub_Repository = new Mock<IEF_Repository<Entidade>>();
            _stub_Service = new Mock<EF_Service<Entidade>>(_stub_Repository.Object);
            _mock_controller = new Mock<EntidadeController>(_stub_Service.Object);
        }

        [Fact]
        public void Get_ReturnsAnyValue()
        {
            var actual = _mock_controller.Object.Get(null).Result;
            Assert.NotNull(actual);
        }

        [Fact]
        public void Post_ReturnsAnyValue()
        {
            var actual = _mock_controller.Object.Post(null).Result;
            Assert.NotNull(actual);
        }

        [Fact]
        public void Put_ReturnsAnyValue()
        {
            var actual = _mock_controller.Object.Put(null).Result;
            Assert.NotNull(actual);
        }

        [Fact]
        public void Delete_ReturnsAnyValue()
        {
            var actual = _mock_controller.Object.Delete(null).Result;
            Assert.NotNull(actual);
        }


    }
}
