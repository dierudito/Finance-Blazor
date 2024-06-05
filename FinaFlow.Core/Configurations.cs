using System.Net;

namespace FinaFlow.Core;

public static class Configurations
{
    public const int DefaultStatusCode = (int)HttpStatusCode.OK;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;

    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
}
