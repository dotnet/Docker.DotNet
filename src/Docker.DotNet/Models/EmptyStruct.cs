namespace Docker.DotNet.Models
{
    /// <summary>
    /// In go something like map[string]struct{} has no correct match so we create the
    /// empty type here that can be used for something like IDictionary&lt;string, EmptyStruct&gt;.
    /// </summary>
    public struct EmptyStruct
    {
    }
}