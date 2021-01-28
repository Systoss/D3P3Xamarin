using Amazon.Runtime;
using System.Net.Http;

namespace BlankApp1.Droid
{
    public class AndroidClientFactory : HttpClientFactory
    {
        public override HttpClient CreateHttpClient(IClientConfig clientConfig)
        {
            return new HttpClient();
        }
    }
}