using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpDemo.Base;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestSharpDemo.Steps
{
    [Binding]
    class CreatePost
    {
        private Settings _settings;
        public CreatePost(Settings settings) => _settings = settings;

        [Given(@"The user gets authenticated for ""(.*)""")]
        public void GivenTheUserGetsAuthenticatedFor(string url)
        {
            _settings.Request = new RestRequest(url, Method.POST);

            var file = @"TestData\User.json";
            var jsonData = JsonConvert.DeserializeObject<LoginUser>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            _settings.Request.AddJsonBody(jsonData);

            _settings.Response = _settings.RestClient.ExecutePostTaskAsync(_settings.Request).GetAwaiter().GetResult();
            var access_token = _settings.Response.DeserializeResponse()["token"];

            var token = access_token.Split(' ')[1];

            var jwtAuth = new JwtAuthenticator(token);
            _settings.RestClient.Authenticator = jwtAuth;
        }


        [Then(@"I create a post request to ""(.*)"" with text ""(.*)""")]
        public void ThenICreateAPostRequestToWithText(string url, string PostText)
        {
            _settings.Request = new RestRequest(url, Method.POST);
            _settings.Request.AddJsonBody(new { text = PostText });

            _settings.Response = _settings.RestClient.ExecutePostTaskAsync(_settings.Request).GetAwaiter().GetResult();
        }


        [Then(@"I should see the post as ""(.*)""")]
        public void ThenIShouldSeeThePostAs(string text)
        {
            var reponseText = _settings.Response.DeserializeResponse()["text"];
            Assert.That(reponseText, Is.EqualTo(text), "Post value does not match.");
        }

    }
}
