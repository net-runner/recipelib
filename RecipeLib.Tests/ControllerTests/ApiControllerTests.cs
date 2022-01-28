using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace RecipeLib.Tests.ControllerTests
{
    public class ApiControllerTests
    {
        [Fact]
        public async Task ApiRoute_Should_Return_ResponseOk()
        {
            var factory = new WebApplicationFactory<Startup>();
            var httpclient = factory.CreateClient();

            var response = await httpclient.GetAsync("/api/");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
