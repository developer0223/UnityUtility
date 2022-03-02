namespace developer0223.WebRequestor
{
    /// <summary>
    /// Please see details at : https://developer.mozilla.org/ko/docs/Web/HTTP
    /// </summary>

    public class ResponseCode
    {
        // 200 ~
        internal const long SUCCESS = 200;
        internal const long CREATED = 201;
        internal const long ACCEPTED = 202;
        
        // 400 ~
        internal const long BAD_REQUEST = 400;
        internal const long UNAUTHORIZED = 401;
        internal const long PAYMENT_REQUIRED = 402;
        internal const long FORBIDDEN = 403;
        internal const long PAGE_NOT_FOUND = 404;
        internal const long METHOD_NOT_ALLOWED = 405;
        internal const long NOT_ACCEPTABLE = 406;
        internal const long REQUEST_TIMEOUT = 408;

        // 500 ~
        internal const long SERVER_ERROR = 500;
        internal const long NOT_IMPLEMENTED = 501;
        internal const long BAD_GATEWAY = 502;
        internal const long SERVICE_UNAVAILABLE = 503;
    }
}