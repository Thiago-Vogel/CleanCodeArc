using AppCore.Models;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Web;
using Web.Controllers;
using Xunit;

namespace FunctionalTests.Controllers
{
    public class EntidadeController_tests : IClassFixture<WebApplicationFactory<Startup>>
    {
        HttpClient client { get; }

        public EntidadeController_tests(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async void Get_ReturnsEntidadeArray()
        {
            var response = await client.GetAsync("/Entidade/Get");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<IEnumerable<Entidade>>(stringResponse);
            Assert.IsAssignableFrom<IEnumerable<Entidade>>(obj);
        }

        [Fact]
        public async void GetByFilter_ReturnsEntidadeArray()
        {
            var texto = "teste";
            var response = await client.GetAsync("/Entidade/Get" + "?texto=" + texto);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<IEnumerable<Entidade>>(stringResponse);
            Assert.IsAssignableFrom<IEnumerable<Entidade>>(obj);
        }

        [Fact]
        public async void Add_ReturnsEntidade()
        {
            var entidade = new Entidade
            {
                Id = 0,
                texto = "testeFunc",
                data = null,
                valor = 0,
                ativo = true
            };
            var jsonObj = JsonConvert.SerializeObject(entidade);
            var response = await client.PostAsync("/Entidade/Post", new StringContent(jsonObj, Encoding.UTF8,"application/json"));
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Entidade>(stringResponse);
            Assert.IsAssignableFrom<Entidade>(obj);
        }

        [Fact]
        public async void Edit_ReturnsEntidade()
        {
            var response = await client.GetAsync("/Entidade/Get");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var entidades = JsonConvert.DeserializeObject<IEnumerable<Entidade>>(stringResponse);

            if (entidades.Any())
            {
                var entidade = entidades.FirstOrDefault();
                entidade.texto = "testeFunc123";
                var jsonObj = JsonConvert.SerializeObject(entidade);

                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(jsonObj, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(client.BaseAddress.AbsoluteUri + "Entidade/Put")
                };

                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                stringResponse = await response.Content.ReadAsStringAsync();
                var _obj = JsonConvert.DeserializeObject<Entidade>(stringResponse);
                Assert.IsAssignableFrom<Entidade>(_obj);
            }
        }

        [Fact]
        public async void Delete_ReturnsEntidade()
        {
            var response = await client.GetAsync("/Entidade/Get");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var entidades = JsonConvert.DeserializeObject<IEnumerable<Entidade>>(stringResponse);
            
            if (entidades.Any())
            {
                var jsonObj = JsonConvert.SerializeObject(entidades.FirstOrDefault());
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(jsonObj, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(client.BaseAddress.AbsoluteUri + "Entidade/Delete")
                };

                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                stringResponse = await response.Content.ReadAsStringAsync();
                var _obj = JsonConvert.DeserializeObject<Entidade>(stringResponse);
                Assert.IsAssignableFrom<Entidade>(_obj);
            }
        }

    }
}
