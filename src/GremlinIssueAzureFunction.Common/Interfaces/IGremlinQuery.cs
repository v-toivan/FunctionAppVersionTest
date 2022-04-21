using ExRam.Gremlinq.Core;

namespace GremlinIssueAzureFunction.Common.Interfaces
{
    public interface IGremlinQuery
    {
        IGremlinQuerySource GetGremlinQuerySource();
    }
}
