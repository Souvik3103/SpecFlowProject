using RestSharp;

namespace RestSharpAPIProject.Utils
{
    public class RequestLibrary
    {
        public RestRequest createRequest(string endpoint)
        {

            RestRequest request = new RestRequest(endpoint)
            {
                Method = Method.Get
            };
            request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }

            });
            return request;
        }

        public RestRequest AddRequestBody(string filePath, RestRequest request)
        {
            string requestBody = File.ReadAllText(filePath);
            request.AddBody(requestBody);
            return request;
        }

        public async Task<RestResponse> GetResponseAsync(RestClient restClient, RestRequest restRequest)
        {
            return await restClient.ExecuteAsync(restRequest);
        }
    }
}
