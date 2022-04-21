using System;
using System.Security.Cryptography;
using System.Text;
using GremlinIssueAzureFunction.Common.Interfaces;

namespace GremlinIssueAzureFunction.Common
{
    public class PartitionKeyHelper : IPartitionKeyHelper
    {
        
        private readonly int _noOfBucket=6;

        // public PartitionKeyHelper(IConfiguration configuration)
        // {
        //     _noOfBucket =  configuration.GetValue<int>("NumberOfBucket");
        // }
        // public PartitionKeyHelper(int num)
        // {
        //     _noOfBucket = num; 
        // }
        public string GetBucketFromRecordId(string id)
        {
            var partitionKey = BitConverter.ToInt32(Encoding.UTF8.GetBytes(id), 0);
            return CreateAndGetHash(partitionKey, _noOfBucket);
                
        }
        private string CreateAndGetHash(int partitionKey, int noOfBucket)
        {
            var sha256 = SHA256.Create();
            var bucket = partitionKey % noOfBucket;
            var partition = sha256.ComputeHash(BitConverter.GetBytes(bucket));
            var hash = GetStringFromHash(partition);
            return hash;
        }

        private string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            for (var i = 0; i < 10; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
