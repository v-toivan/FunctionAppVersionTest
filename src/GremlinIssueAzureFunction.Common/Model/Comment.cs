using System;

namespace GremlinIssueAzureFunction.Common.Model
{
    public interface IPerson : IVertex
    {
         string Id { get; set; }
         string Label { get; set; }
         string CreatorId { get; set; }
         string BucketNo { get; set; }
         DateTime LastUpdatedDateTime { get; set; }
         DateTime CreationDateTime { get; set; }
         bool IsDeleted { get; set; }
         int? ttl { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
    }

    public class Person : IPerson
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string CreatorId { get; set; }
        public string BucketNo { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public int? ttl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
