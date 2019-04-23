using CodeChallengeV2.Models;
using CodeChallengeV2.Services;
using CodeChallengeV2.Tests.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CodeChallengeV2.Tests
{
    public class NetworkSimulatorServiceTest
    {
        //[Theory]
        //[JsonFileData("graph1.json")]        
        //public async void FindCritialGateways(NetworkGraph graph)
        //{
        //    var res = await new NetworkSimulatorService().FindCritialGateways(graph);

        //    Assert.Equal("gw001", res.FirstOrDefault().Id);
        //}

        [Theory]
        [JsonFileData("graph2.json")]
        public async void FindCritialGateways2(NetworkGraph graph)
        {
            var res = await new NetworkSimulatorService().FindCritialGateways(graph);

            Assert.Null(res.FirstOrDefault());
        }
    }
}
