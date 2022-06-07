using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using Newtonsoft.Json;

using Swaraj.Domain.Entities;
using Swaraj.Presentation;

using Xunit;

namespace Test.Integration.API
{
    public class WorkItemsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;

        public WorkItemsControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();

            Seed();
        }

        private void Seed()
        {
            var workitems = new List<WorkItem>()
            {
                WorkItem.New(),
                WorkItem.New(),
                WorkItem.New(),
                WorkItem.New(),
                WorkItem.New(),
            };

            foreach (var item in workitems)
            {
                var json = JsonConvert.SerializeObject(item);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var responseTask = _client.PostAsync("api/workitems", data);

                responseTask.Wait();

                var response = responseTask.Result;

            }
        }

        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var response = await _client.GetAsync("api/workitems");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var workItems = JsonConvert.DeserializeObject<WorkItem[]>(await response.Content.ReadAsStringAsync());

            workItems.Should().HaveCount(5);
        }
    }
}
