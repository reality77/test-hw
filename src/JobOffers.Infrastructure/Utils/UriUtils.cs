using System.Web;

namespace JobOffers.Infrastructure.Utils;

public static class UriUtils
{
    /// <summary>
    /// Adds a query parameter to a URI.
    /// </summary>
    /// <param name="paramName">The name of the query parameter.</param>
    /// <param name="paramValue">The value of the query parameter.</param>
    /// <param name="isFirstParameter">Indicates if this is the first parameter in the URI.</param>
    /// <returns>The query parameter string to be appended to the URI.</returns>
    public static Uri AddQueryParameter(this Uri uri, string paramName, string? paramValue)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri), "URI cannot be null.");
        }

        if (string.IsNullOrEmpty(paramName))
        {
            throw new ArgumentException("Parameter name cannot be null or empty.", nameof(paramName));
        }

        if (string.IsNullOrEmpty(paramValue))
        {
            return uri;
        }

        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[paramName] = paramValue;
        uriBuilder.Query = query.ToString();

        return uriBuilder.Uri;
    }
}