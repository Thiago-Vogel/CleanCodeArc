using AppCore.Dtos;
using AppCore.Implementations;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    public class EntidadeController:ControllerBase
    {
        IEF_Service<Entidade> entidades;
        public EntidadeController(IEF_Service<Entidade> _entidades)
        {
            entidades = _entidades;
        }
        
        [HttpGet]
        [Route("Entidade/Get")]
        public async Task<string> Get([FromQuery] EntidadeParams query)
        {
            query = query != null ? query : new EntidadeParams();
            Expression<Func<Entidade, bool>> expression = e =>
                 e.ativo == query.ativo &&
                 (e.Id == query.id || query.id == 0) &&
                 (e.data >= query.data || query.data == null) &&
                 (e.texto == query.texto || query.texto == null) &&
                 (e.valor >= query.valor || query.valor == 0);
        
            var obj = await entidades.GetAsync(expression,query.limit,query.page);
            return JsonConvert.SerializeObject(obj);
        }
        
        [HttpPost]
        [Route("Entidade/Post")]
        public async Task<string> Post(Entidade entity)
        {
            var obj = await entidades.AddAsync(entity);
            return JsonConvert.SerializeObject(obj);
        }
        
        [HttpPut]
        [Route("Entidade/Put")]
        public async Task<string> Put(Entidade entity)
        {
            var obj = await entidades.UpdateAsync(entity);
            return JsonConvert.SerializeObject(obj);
        }
        
        [HttpDelete]
        [Route("Entidade/Delete")]
        public async Task<string> Delete(Entidade entity)
        {
            var obj = await entidades.DeleteAsync(entity);
            return JsonConvert.SerializeObject(obj);
        }

    }
}
