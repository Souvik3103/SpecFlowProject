using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Utils;
using NUnit.Framework;
using RestSharp;
using RestSharpAPIProject.Drivers;
using RestSharpAPIProject.Utils;
using System;
using TechTalk.SpecFlow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestSharpAPIProject.StepDefinitions
{
    [Binding]
    public class RestTestStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private RestClient _restClient;

        RequestLibrary helper;
        private Driver drivers;
        private ExtentReports _reporter;
        private RestResponse _restResponse;

        public RestTestStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            drivers = new Driver(scenarioContext);
            _restClient = _scenarioContext.Get<RestClient>("RestClient");
            _reporter = _scenarioContext.Get<ExtentReports>("Reporter");
            helper = new RequestLibrary();
        }

        [Given(@"User creates a request with endpoint ""([^""]*)""")]
        public async Task GivenUserCreatesARequestWithEndpoint(string endpoint)
        {
            var request = helper.createRequest(endpoint);
            RestResponse response = await helper.GetResponseAsync(_restClient, request);
            var content = response.Content;
            _restResponse = response;
            Assert.That(response.IsSuccessful, "Api call was not successful");
        }

        [Then(@"User Checks the status to be ""([^""]*)""")]
        public void ThenUserChecksTheStatusToBe(string status)
        {
            Assert.That(_restResponse.StatusCode == System.Net.HttpStatusCode.OK, "Status code is not OK (200)");
        }

        [Then(@"User Checks the response to have a valid body")]
        public void ThenUserChecksTheResponseToHaveAValidBody()
        {
            var content = _restResponse.Content;
            Assert.That(!content.IsNullOrEmpty(), "Response Body is empty");
            Assert.That(!_restResponse.Headers.IsNullOrEmpty(), "Response header is empty");
            Assert.That(_restResponse.ContentLength > 0, "Content Length is zero");
            Assert.That(content.GetType() != null, "Content Type is null");
        }

        [Then(@"User Checks the Availability of (.*) to be (.*)")]
        public void ThenUserChecksTheAvailabilityOfBook(string bookName, string status)
        {
            bool availability = false;
            var content = JArray.Parse(_restResponse.Content);
            for (int i = 0; i < content.Count(); i++)
            {
                var bookNameResp = content[i]["name"].ToString();
                if (bookNameResp.ToUpper().Equals(bookName.ToUpper()))
                {
                    availability = bool.Parse(content[i]["available"].ToString());
                    break;
                }
            }
            Assert.That(availability == bool.Parse(status), $"Availability of book {bookName} is not as expected {status} but {availability.ToString()}");
        }

        [Then(@"User Checks the (.*) to be of Type (.*)")]
        public void ThenUserChecksTheBookToBeOfType(string bookName, string type)
        {
            string typeOfBook = string.Empty;
            var content = JArray.Parse(_restResponse.Content);
            for (int i = 0; i < content.Count(); i++)
            {
                var bookNameResp = content[i]["name"].ToString();
                if (bookNameResp.ToUpper().Equals(bookName.ToUpper()))
                {
                    typeOfBook = content[i]["type"].ToString();
                    break;
                }
            }
            Assert.That(typeOfBook.Equals(type), $"Type of book {bookName} is not as expected {type} but {typeOfBook}");
        }







    }
}
