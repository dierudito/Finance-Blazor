﻿using System.Text.Json.Serialization;

namespace FinaFlow.Core.Response;
public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configurations.DefaultPageSize) : base(data)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public PagedResponse(TData? data, int code = Configurations.DefaultStatusCode, string? message = null) : base(data, code, message)
    {        
    }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = Configurations.DefaultPageSize;
    public int TotalCount { get; set; }
}