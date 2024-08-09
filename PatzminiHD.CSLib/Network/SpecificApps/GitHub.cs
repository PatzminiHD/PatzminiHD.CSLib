using Newtonsoft.Json;
using PatzminiHD.CSLib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Network.SpecificApps
{
    /// <summary>
    /// Contains network methods specific to GitHub
    /// </summary>
    public class GitHub
    {
        /// <summary>
        /// Represents a Asset in a GitHub Release
        /// </summary>
        public struct GitHubReleaseAssets
        {
            /// <summary> The API url of the asset </summary>
            public string url;
            /// <summary> The filename of the asset </summary>
            public string name;
            /// <summary> The download url of the asset </summary>
            public string browser_download_url;
        }
        /// <summary>
        /// Represents a GitHub Release
        /// </summary>
        public struct GitHubRelease
        {
            /// <summary> The API url of the release </summary>
            public string url;
            /// <summary> The browser url of the release </summary>
            public string html_url;
            /// <summary> The name of the release </summary>
            public string name;
            /// <summary> The tag name of the release </summary>
            public string tag_name;
            /// <summary> True if the release is a draft </summary>
            public bool draft;
            /// <summary> True if the release is a prerelease </summary>
            public bool prerelease;
            /// <summary> The body (description) of the release </summary>
            public string body;
            /// <summary> The list of files that have been added to the release </summary>
            public List<GitHubReleaseAssets> assets;
        };
        /// <summary>
        /// Get the latest release of a specified repository
        /// </summary>
        /// <param name="repoOwner">The account or organization name that owns the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="accessToken">The access token if the repository is private</param>
        public static Variant<GitHubRelease, string> GetLatestRelease(string repoOwner, string repoName, string? accessToken = null)
        {
            HttpRequestMessage httpRequestMessage = new();
            httpRequestMessage.Method = HttpMethod.Get;
            httpRequestMessage.RequestUri = new Uri($"https://api.github.com/repos/{repoOwner}/{repoName}/releases/latest");
            httpRequestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            httpRequestMessage.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
            httpRequestMessage.Headers.Add("User-Agent", $"{Info.Name}/{Info.Version} ({Environment.Get.OsString})");

            if (accessToken != null)
                httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = Http.GetRequest(httpRequestMessage);

            var responeJson = response.Content.ReadAsStringAsync();
            responeJson.Wait();

            var responseString = response.Content.ReadAsStringAsync();
            responseString.Wait();

            if (!response.IsSuccessStatusCode)
                return responseString.Result;

            GitHubRelease release = new();

            //TODO: Parse response

            dynamic? jsonObject = JsonConvert.DeserializeObject(responseString.Result);

            if (jsonObject == null)
                return $"Json object could not be deserialised: {responseString.Result}";

            try
            {
                release.url = jsonObject.url;
                release.html_url = jsonObject.html_url;
                release.name = jsonObject.name;
                release.tag_name = jsonObject.tag_name;
                release.draft = jsonObject.draft;
                release.prerelease = jsonObject.prerelease;
                release.body = jsonObject.body;
                List<GitHubReleaseAssets> assets = new List<GitHubReleaseAssets>();

                foreach (var jsonAsset in jsonObject.assets)
                {
                    GitHubReleaseAssets asset = new GitHubReleaseAssets();
                    asset.url = jsonAsset.url;
                    asset.name = jsonAsset.name;
                    asset.browser_download_url = jsonAsset.browser_download_url;
                    assets.Add(asset);
                }

                release.assets = assets;
            }
            catch (Exception ex)
            {
                return $"Json object could not be parsed: {ex.Message}";
            }

            return release;
        }
    }
}
