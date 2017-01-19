namespace Docker.DotNet.Models
{
    public struct ImageOperationProgress
    {
        public string Status { get; set; }
        public int Current { get; set; }
        public int Total { get; set; }
    }
}
