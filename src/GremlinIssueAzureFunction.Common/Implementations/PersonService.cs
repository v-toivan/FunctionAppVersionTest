using System;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core;
using GremlinIssueAzureFunction.Common.Interfaces;
using GremlinIssueAzureFunction.Common.Model;

namespace GremlinIssueAzureFunction.Common.Implementations
{
    public class PersonService:IPersonService
    {
        private readonly IGremlinQuerySource _g;
        private readonly IPartitionKeyHelper _partitionKeyHelper;

        public PersonService(IGremlinQuery gremlinQuery,IPartitionKeyHelper partitionKeyHelper)
        {
            _partitionKeyHelper = partitionKeyHelper;
           _g= gremlinQuery.GetGremlinQuerySource();
        }
        public async Task Add()
        {
            var id = Guid.NewGuid().ToString("N");
            var bucketNo = _partitionKeyHelper.GetBucketFromRecordId(id);
            var uctNow = DateTime.UtcNow;
            var person = new Person()
            {
                Id = id,
                BucketNo = bucketNo,
                FirstName = "First Name " + Guid.NewGuid(),
                LastName = "First Name " + Guid.NewGuid(),
                LastUpdatedDateTime = uctNow,
                CreationDateTime = uctNow,
                CreatorId = id
            };
           await _g.AddV<Person>(person);
        }
    } 
}
