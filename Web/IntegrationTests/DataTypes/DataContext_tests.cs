using AppCore.Models;
using Infrastructure.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace IntegrationTests.DataTypes
{
    public class DataContext_tests
    {
        EF_Context _context;

        public DataContext_tests()
        {
            string connection = "Server=(localdb)\\MSSQLLocalDB;DataBase=Entidades;Integrated Security=true;MultipleActiveResultSets=True";
            var dbOptions = new DbContextOptionsBuilder<EF_Context>()
                 .UseSqlServer(connection, opt => opt.MigrationsAssembly("Infrastructure"));
            _context = new EF_Context(dbOptions.Options);
        }

        [Fact]
        public void Add_Writes_Entidade_ToDevelopmentDataBase() 
        {
            var entidade = new Entidade
            {
                texto = "teste",
                valor = 0,
                data = null,
                ativo = true,
                Id = 0
            };
            var result = _context.Set<Entidade>().Add(entidade);
            _context.SaveChanges();
            Assert.NotNull(result.Entity);
        }

        [Fact]
        public void Get_Reads_Entidade_FromDevelopmentDataBase()
        {
            var result = _context.Set<Entidade>().ToList();
            Assert.NotNull(result);
        }


        [Fact]
        public void Edit_Updates_Entidade_OnDevelopmentDataBase()
        {
            var _entidade = _context.Set<Entidade>().FirstOrDefault();
            if (_entidade != null)
            {
                _entidade.texto = "teste123";
                var result = _context.Set<Entidade>().Update(_entidade);
                _context.Entry(result.Entity).State = EntityState.Modified;
                _context.SaveChanges();
                Assert.NotNull(result.Entity);
            }
        }

        [Fact]
        public void Remove_Deletes_Entidade_FromDevelopmentDataBase()
        {
            var _entidade = _context.Set<Entidade>().FirstOrDefault();
            if (_entidade != null)
            {
                var result = _context.Set<Entidade>().Remove(_entidade);
                _context.SaveChanges();
                Assert.NotNull(result.Entity);
            }
        }

    }
}
