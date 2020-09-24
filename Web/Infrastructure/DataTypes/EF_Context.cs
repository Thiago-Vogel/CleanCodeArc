using AppCore.Implementations;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataTypes
{
    public class EF_Context:DbContext
    {
        public EF_Context(DbContextOptions<EF_Context> options):base(options)
        {

        }
        DbSet<Entidade> Entidades { get; set; }
    }
}
