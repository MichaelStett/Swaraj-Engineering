using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Respawn;

using Xunit;

namespace Test.Integration.API
{
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly Checkpoint _checkpoint = new()
        {
            SchemasToInclude = new[]
            {
                "Playground"
            },
            WithReseed = true
        };

        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();

            _checkpoint.Reset(_factory.Configuration.GetConnectionString("SQL")).Wait();
        }
    }
}
