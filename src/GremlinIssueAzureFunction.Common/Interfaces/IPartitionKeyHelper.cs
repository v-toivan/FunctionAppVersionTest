namespace GremlinIssueAzureFunction.Common.Interfaces
{
    public interface IPartitionKeyHelper
    {
        string GetBucketFromRecordId(string id);
    }
}
