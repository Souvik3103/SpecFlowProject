using RestSharp;

namespace RestSharpAPIProject.Drivers
{
    public class Driver
    {
        public Driver(ScenarioContext scenarioContext)
        {
            var restClientOptions = new RestClientOptions
            {
                BaseUrl = new Uri("https://simple-books-api.glitch.me"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };

            var restClient = new RestClient(restClientOptions);

            scenarioContext.Add("RestClient", restClient);
        }
    }
}
