﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;

namespace RestSharpDemo
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void GetUser()
        {
            var client = new RestClient("https://reqres.in/api/");

            var request = new RestRequest("users/{userid}", Method.GET);
            request.AddUrlSegment("userid", 5);

            var response = client.Execute(request);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["data"];

            //JObject obs = JObject.Parse(response.Content);
            //Assert.That(obs["data"]["email"].ToString(), Is.EqualTo("charles.morris@reqres.in"), "Email id is not matched.");
            var result = response.GetResponseObjects("data", "first_name");
            Assert.That(result, Is.EqualTo("Charles"), "Email not matched");
        }

        [Test]
        public void PostWithAnonymousBody()
        {
            var client = new RestClient("https://reqres.in/api/");

            var request = new RestRequest("users", Method.POST);
            request.AddJsonBody(new { name = "Shashank", job = "Developer" });

            var response = client.Execute(request);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["name"];
            var result = response.DeserializeResponse()["name"];

            Assert.That(result, Is.EqualTo("Shashank"), "User name is incorrect");
        }

        [Test]
        public void PostWithTypeClassBody()
        {
            var client = new RestClient("https://reqres.in/api/");

            var request = new RestRequest("users", Method.POST);
            request.AddJsonBody(new User() { name = "Barry", job = "Running"});

            var response = client.Execute<User>(request);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["name"];

            Assert.That(response.Data.name, Is.EqualTo("Barry"), "User name is incorrect");
        }

        [Test]
        public void PostWithAsync()
        {
            var client = new RestClient("https://reqres.in/api/");

            var request = new RestRequest("users", Method.POST);
            request.AddJsonBody(new User() { name = "Bruce", job = "Vigilant" });

            var response = client.ExecutePostTaskAsync<User>(request).GetAwaiter().GetResult();

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["name"];

            Assert.That(response.Data.name, Is.EqualTo("Bruce"), "User name is incorrect");
        }

        [Test]
        public void PostWithJSON()
        {
            var client = new RestClient("https://reqres.in/api/");

            var request = new RestRequest("users", Method.POST);

            var file = @"TestData\Data.json";
            var jsonData = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            request.AddJsonBody(jsonData);

            var response = client.ExecutePostTaskAsync<User>(request).GetAwaiter().GetResult();

            Assert.That(response.Data.name, Is.EqualTo("Penguin"), "Name is not matched.");
        }
    }
}
