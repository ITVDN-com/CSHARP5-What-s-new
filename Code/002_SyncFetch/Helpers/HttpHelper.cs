using System;
using System.Linq;
using System.Net;

namespace AsyncFetch.Helpers
{
    static class HttpHelper
    {
        public static string FormatHeaders(this WebHeaderCollection headers)
        {
            var headerStrings = from header in headers.Keys.Cast<string>()
                                select string.Format("{0}: {1}", header, headers[header]);

            return string.Join(Environment.NewLine, headerStrings.ToArray());
        }
    }
}
