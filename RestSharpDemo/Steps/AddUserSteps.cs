using NUnit.Framework;
using RestSharp;
using RestSharpDemo.Base;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestSharpDemo.Steps
{
    [Binding]
    class AddUserSteps
    {
        private Settings _settings;
        public AddUserSteps(Settings settings) => _settings = settings;

        [Given(@"I perform POST operation for ""(.*)""")]
        public void GivenIPerformPOSTOperationFor(string url)
        {
            _settings.Request = new RestRequest(url, Method.POST);
        }

        [Given(@"I perform operation with body")]
        public void GivenIPerformOperationWithBody(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request.AddJsonBody(new User() { name = data.name.ToString(), job = data.job.ToString() });
            _settings.Response = _settings.RestClient.ExecutePostTaskAsync<User>(_settings.Request).GetAwaiter().GetResult();
            //var result = _settings.Response.DeserializeResponse();
            //foreach(KeyValuePair<string, string> userData in result)
            //{
            //    Console.WriteLine($"{userData.Key} is {userData.Value}");
            //}
        }

        [Then(@"I should see the ""(.*)"" as ""(.*)""")]
        public void ThenIShouldSeeTheAs(string key, string value)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is not matched");
        }

    }
}
