using AppCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Implementations
{
    public interface IEF_Context
    {
        DbSet<Entidade> Entidades {get; set;}
    }
}
