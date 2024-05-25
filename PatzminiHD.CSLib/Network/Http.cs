using System.Net.WebSockets;

namespace PatzminiHD.CSLib.Network
{
    /// <summary>
    /// Contains Methods for Http requests
    /// </summary>
    public class Http
    {
        /// <summary>
        /// Send a HTTP Post request to a specified url with specified data
        /// </summary>
        /// <param name="url">The url or ip address of the server</param>
        /// <param name="values">The values of the request</param>
        /// <returns>The result of the request</returns>
        //https://stackoverflow.com/questions/4015324/send-http-post-request-in-net
        public static string PostRequest(string url, Dictionary<string, string> values)
        {
            HttpClient client = new();
            HttpContent content = new FormUrlEncodedContent(values);

            var response = client.PostAsync(url, content);
            response.Wait();

            var responseString = response.Result.Content.ReadAsStringAsync();
            responseString.Wait();

            client.Dispose();

            return responseString.Result;
        }
        /// <summary>
        /// Send a HTTP Get request to a specified url with specified data
        /// </summary>
        /// <param name="url">The url or ip address of the server</param>
        /// <returns>The result of the request</returns>
        //https://stackoverflow.com/questions/4015324/send-http-post-request-in-net
        public static string GetRequest(string url)
        {
            HttpClient client = new();

            var responseString = client.GetStringAsync(url);
            responseString.Wait();

            client.Dispose();

            return responseString.Result;
        }
    }
}
