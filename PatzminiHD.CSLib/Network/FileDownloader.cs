namespace PatzminiHD.CSLib.Network
{
    /// <summary>
    /// Download files while monitoring progress
    /// </summary>
    public class FileDownloader
    {
        /// <summary>
        /// An event that fires every time the download progresses
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs>? DownloadProgess;
        /// <summary>
        /// An event that fires every time a download finishes
        /// </summary>
        public event EventHandler<DownloadFinishedEventArgs>? DownloadFinished;
        /// <summary>
        /// An event that fires every time a download failes
        /// </summary>
        public event EventHandler<DownloadFailedEventArgs>? DownloadFailed;

        private List<(string name, string value)>? _customHeaders;

        /// <summary>
        /// Create a new instance of the FileDownloader object
        /// </summary>
        /// <param name="customHeaders">Optional, custom headers added to each download request</param>
        public FileDownloader(List<(string name, string value)>? customHeaders = null)
        {
            _customHeaders = customHeaders;
        }

        /// <summary>
        /// Download a file from a given url to a given Path
        /// </summary>
        /// <param name="url">The URL where to download from</param>
        /// <param name="localPath">The local path where to save the file (with filename and extension)</param>
        /// <param name="overwrite">True if an existing local file with the same path should be overwritten</param>
        /// <param name="createSubfolders">True if nonexistent subfolders in <paramref name="localPath"/> should be created</param>
        public async Task<bool> Download(string url, string localPath, bool overwrite = false, bool createSubfolders = false)
        {
            var directoryName = Path.GetDirectoryName(localPath);
            if (File.Exists(localPath))
            {
                if (overwrite)
                    File.Delete(localPath);
                else
                {
                    DownloadFailed?.Invoke(this, new DownloadFailedEventArgs(url, localPath,
                        $"File already exists and overwriting has not been allowed"));
                    return false;
                }
            }
            else if (!Directory.Exists(directoryName))
            {
                if(createSubfolders && directoryName != null)
                    Directory.CreateDirectory(directoryName);
                else
                {
                    DownloadFailed?.Invoke(this, new DownloadFailedEventArgs(url, localPath,
                        $"Parent directory of {nameof(localPath)} does not exist and creating subfolders has not been allowed"));
                    return false;
                }
            }

            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.RequestUri = new Uri(url);
                httpRequestMessage.Headers.Add("User-Agent", $"{Info.Name}/{Info.Version} ({Environment.Get.OsString})");

                if (_customHeaders != null)
                    foreach (var header in _customHeaders)
                        httpRequestMessage.Headers.Add(header.name, header.value);

                using (HttpResponseMessage response = await client.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        DownloadFailed?.Invoke(this, new DownloadFailedEventArgs(url, localPath, response.StatusCode.ToString()));
                        return false;
                    }
                    try
                    {
                        long? totalBytes = response.Content.Headers.ContentLength;

                        using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                            fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                        {
                            byte[] buffer = new byte[8192];
                            long totalBytesRead = 0;
                            int bytesRead;

                            while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await fileStream.WriteAsync(buffer, 0, bytesRead);
                                totalBytesRead += bytesRead;

                                if (totalBytes.HasValue)
                                {
                                    DownloadProgess?.Invoke(this, new DownloadProgressEventArgs(url, localPath, (byte)(((double)totalBytesRead / totalBytes.Value) * 100)));
                                }
                            }

                            DownloadFinished?.Invoke(this, new DownloadFinishedEventArgs(url, localPath));
                        }
                    }
                    catch (Exception ex)
                    {
                        DownloadFailed?.Invoke(this, new DownloadFailedEventArgs(url, localPath, ex.ToString()));
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// The EventArgs for the <see cref="FileDownloader.DownloadProgess"/> event
        /// </summary>
        public class DownloadProgressEventArgs(string url, string filePath, byte progress) : EventArgs
        {
            /// <summary> The URL where the file was downloaded from </summary>
            public string Url { get; } = url;
            /// <summary> The Local path the file should be downloaded to </summary>
            public string FilePath { get; } = filePath;
            /// <summary> The Download progress in percent from 0 to 100 </summary>
            public byte Progress { get; } = progress;
        }
        /// <summary>
        /// The EventArgs for the <see cref="FileDownloader.DownloadFinished"/> event
        /// </summary>
        public class DownloadFinishedEventArgs(string url, string filePath) : EventArgs
        {
            /// <summary> The URL where the file was downloaded from </summary>
            public string Url { get; } = url;
            /// <summary> The Local path the file should be downloaded to </summary>
            public string FilePath { get; } = filePath;
        }
        /// <summary>
        /// The EventArgs for the <see cref="FileDownloader.DownloadFailed"/> event
        /// </summary>
        public class DownloadFailedEventArgs(string url, string filePath, string? failReason = null) : EventArgs
        {
            /// <summary> The URL where the file was downloaded from </summary>
            public string Url { get; } = url;
            /// <summary> The Local path the file should be downloaded to </summary>
            public string FilePath { get; } = filePath;
            /// <summary> The reason why the download has failed (if available) </summary>
            public string? FailReason { get; } = failReason;
        }
    }
}
