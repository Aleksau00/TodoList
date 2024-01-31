using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.RegularExpressions;

namespace Novalite.Todo.Shared.Services
{
    public class BlobStorageService
    {
        private readonly string _storageAccountConnectionString;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            _storageAccountConnectionString = configuration.GetConnectionString("AzureBlobStorageConnectionString");
            _containerName = "attachments";
        }

        

        public string GenerateSasToken(string blobName, BlobSasPermissions permissions)
        {
            var (accountName, accountKey) = ParseConnectionString(_storageAccountConnectionString);

            var blobServiceClient = new BlobServiceClient(_storageAccountConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _containerName,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddSeconds(30),
            };

            sasBuilder.SetPermissions(permissions);

            var storageSharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);
            var sasToken = sasBuilder.ToSasQueryParameters(storageSharedKeyCredential).ToString();

            return sasToken;
        }

        private (string accountName, string accountKey) ParseConnectionString(string connectionString)
        {
            var match = Regex.Match(connectionString, @"AccountName=([^;]+);AccountKey=([^;]+)");
            if (match.Success)
            {
                return (match.Groups[1].Value, match.Groups[2].Value);
            }

            throw new FormatException("Invalid connection string format");
        }
    }
}
