using AppCore.Implementations;
using AppCore.Models;
using AppCore.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.Services
{
    public class EntidadeService_tests
    {
        Mock<IEF_Repository<Entidade>> _stub_Repository;
        Mock<EF_Service<Entidade>> _mock_service;

        public EntidadeService_tests()
        {
            _stub_Repository = new Mock<IEF_Repository<Entidade>>();
            _mock_service = new Mock<EF_Service<Entidade>>(_stub_Repository.Object);
        }

        [Fact]
        public void Get_CheckForException_ReturnsNull()
        {
            var actual = _mock_service.Object.GetAll().Exception;
            Assert.Null(actual);
        }


        [Fact]
        public void Add_CheckForException_ReturnsNull()
        {
            Entidade obj = new Entidade
            {
                ativo = true,
                data = null,
                texto = "",
                valor = 0
            };
            var actual = _mock_service.Object.AddAsync(obj).Exception;
            Assert.Null(actual);
        }


        [Fact]
        public void Update_CheckForException_ReturnsNull()
        {
            Entidade obj = new Entidade
            {
                Id = 1,
                ativo = true,
                data = null,
                texto = "",
                valor = 0
            };
            var actual = _mock_service.Object.UpdateAsync(obj).Exception;
            Assert.Null(actual);
        }

        [Fact]
        public void Delete_CheckForException_ReturnsNull()
        {
            Entidade obj = new Entidade
            {
                Id = 1,
                ativo = true,
                data = null,
                texto = "",
                valor = 0
            };
            var actual = _mock_service.Object.DeleteAsync(obj).Exception;
            Assert.Null(actual);
        }
    }
}

