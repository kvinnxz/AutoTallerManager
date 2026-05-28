namespace Api.Helpers;

public static class PaginationHelper
{
    public static void AppendHeaders(
        Microsoft.AspNetCore.Http.HttpResponse response,
        int total, int page, int pageSize)
    {
        response.Headers.Append("X-Total-Count", total.ToString());
        response.Headers.Append("X-Page",        page.ToString());
        response.Headers.Append("X-Page-Size",   pageSize.ToString());
        response.Headers.Append("X-Total-Pages", ((int)Math.Ceiling((double)total / pageSize)).ToString());
    }
}
