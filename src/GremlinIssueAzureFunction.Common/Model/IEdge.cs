using System;

namespace GremlinIssueAzureFunction.Common.Model
{
    public interface IEdge
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public int? ttl { get; set; }
        public string Metadata { get; set; }
        public float Weight { get; set; }
        public float DistanceWeight { get; set; }
        public bool ApplyWeightCalc { get; set; }
    }


    public interface IEdgeV2
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public long LastUpdatedDateTime { get; set; }
        public long CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public int? ttl { get; set; }
        public string Metadata { get; set; }
        public float Weight { get; set; }
        public float DistanceWeight { get; set; }
        public bool ApplyWeightCalc { get; set; }
    }

}
