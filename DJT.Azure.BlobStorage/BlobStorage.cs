using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text;

namespace DJT.Azure.BlobStorage
{
    /// <summary>
    /// Calls for ease of use for blob storage
    /// </summary>
    public class BlobStorage
    {
        string _containerName;
        string _connectionString;

        BlobServiceClient blobServiceClient;
        BlobContainerClient containerClient;

        /// <summary>
        /// The blob storage needs
        /// </summary>
        /// <param name="connectionString">A connection string for the blob storage</param>
        /// <param name="containerName">The name of the container, i.e. the type of thing to be stored</param>
        /// <param name="IsPublicFacing">Can the public read these blobs?</param>
        public BlobStorage(string connectionString, string containerName, bool IsPublicFacing = false)
        {
            _containerName = containerName;
            _connectionString = connectionString;
            blobServiceClient = new BlobServiceClient(_connectionString);
            containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            if (IsPublicFacing)
                containerClient.CreateIfNotExists(PublicAccessType.Blob);
            else
                containerClient.CreateIfNotExists(PublicAccessType.None);
        }

        /// <summary>
        /// Store a stream of data as a blob, overwriting if already exists
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="data"></param>
        /// <param name="mimeType"></param>
        /// <returns>The absolute URI of the blob</returns>
        public string UploadBlob(string blobName, Stream data, string? mimeType = null)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            if (!string.IsNullOrWhiteSpace(mimeType))
            {
                BlobHttpHeaders headers = new()
                {
                    ContentType = mimeType
                };
                blobClient.Upload(data, new BlobUploadOptions() { HttpHeaders = headers });
            }
            else
            {
                blobClient.Upload(data, true);
            }
            return blobClient.Uri.AbsoluteUri;
        }

        /// <summary>
        /// Store a string as a blob, overwriting if already exists
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="data"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public bool UploadString(string blobName, string data, string? mimeType = null)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                if (!string.IsNullOrWhiteSpace(mimeType))
                {
                    BlobHttpHeaders headers = new()
                    {
                        ContentType = mimeType
                    };
                    blobClient.Upload(data, new BlobUploadOptions() { HttpHeaders = headers });
                }
                blobClient.Upload(stream, true);
            }
            return true;
        }

        /// <summary>
        /// Download a blob to a stream
        /// </summary>
        /// <param name="blobName"></param>
        /// <param name="readStream"></param>
        /// <returns></returns>
        public bool DownloadBlob(string blobName, Stream readStream)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            if (blobClient.Exists())
            {
                var res = blobClient.DownloadTo(readStream);
                if (res.Status == 200)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// Deletes a blob in this container.
        /// </summary>
        /// <param name="blobName">The name of the blob to delete</param>
        /// <returns>Returns true if the blob was successfully deleted.  Else, false.</returns>
        public bool DeleteBlob(string blobName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            return blobClient.DeleteIfExists().Value;
        }

        /// <summary>
        /// Find a blob by name
        /// </summary>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public List<string> SearchBlobs(string startsWith, bool fullUri = false)
        {
            string endpoint = string.Empty;
            if (fullUri)
            {
                endpoint = containerClient.Uri.ToString();
                if (!endpoint.EndsWith("/")) endpoint += "/";
            }
            return containerClient.GetBlobs(BlobTraits.None, BlobStates.None, startsWith).Select(bi => endpoint + bi.Name).ToList();
        }

    }
}