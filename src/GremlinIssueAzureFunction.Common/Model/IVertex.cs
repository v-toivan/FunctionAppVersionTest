using System;

namespace GremlinIssueAzureFunction.Common.Model
{
    public interface IVertex
    {
         string Id { get; set; }
         string Label { get; set; }
         string CreatorId { get; set; }
         string BucketNo { get; set; }
         DateTime LastUpdatedDateTime { get; set; }
         DateTime CreationDateTime { get; set; }
         bool IsDeleted { get; set; }
         int? ttl { get; set; }
        
    }
}
