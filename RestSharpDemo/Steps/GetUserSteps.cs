using NUnit.Framework;
using RestSharp;
using RestSharpDemo.Base;
using RestSharpDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestSharpDemo.Steps
{
    [Binding]
    class GetUserSteps
    {
        private Settings _settings;
        public GetUserSteps(Settings settings) => _settings = settings;

        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {
            _settings.Request = new RestRequest(url, Method.GET);
        }

        [Given(@"I perform operation for user ""(.*)""")]
        public void GivenIPerformOperationForUser(int userId)
        {
            _settings.Request.AddUrlSegment("userid", userId.ToString());
        }

        [Then(@"I should see ""(.*)"" as ""(.*)""")]
        public void ThenIShouldSeeAs(string key, string value)
        {
            _settings.Response = _settings.RestClient.Execute(_settings.Request);
            Assert.That(_settings.Response.GetResponseObjects("data", key), Is.EqualTo(value), $"The {key} is not matching");
        }
    }
}
