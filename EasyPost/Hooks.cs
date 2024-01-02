using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using EasyPost._base;

namespace EasyPost;

/// <summary>
///     Class representing a set of callbacks to use for introspecting API requests made by an <see cref="EasyPostClient"/>.
/// </summary>
public class Hooks
{
    /// <summary>
    ///     An <see cref="EventHandler{OnRequestExecutingEventArgs}"/> to view an HTTP request by the client prior to being sent.
    ///     Editing the <see cref="HttpRequestMessage"/> in this callback does not impact the <see cref="HttpRequestMessage"/> being executed.
    /// </summary>
    public EventHandler<OnRequestExecutingEventArgs>? OnRequestExecuting { get; set; }

    /// <summary>
    ///     An <see cref="EventHandler{OnRequestResponseReceivedEventArgs}"/> to view an HTTP response received by the client.
    /// </summary>
    public EventHandler<OnRequestResponseReceivedEventArgs>? OnRequestResponseReceived { get; set; }
}

/// <summary>
///     Represents a set of <see cref="EventArgs"/> containing information about an in-flight HTTP request.
///     This set is passed into the <see cref="Hooks.OnRequestExecuting"/> event handler.
/// </summary>
public class OnRequestExecutingEventArgs : EventArgs
{
    /// <summary>
    ///     The <see cref="HttpRequestMessage"/> about to be executed by the HTTP request.
    /// </summary>
    private HttpRequestMessage Request { get; } // Not publicly-exposed, used for reference only.

    /// <summary>
    ///     The <see cref="HttpMethod"/> of the HTTP request.
    /// </summary>
    public HttpMethod Method => Request.Method;

    /// <summary>
    ///     The <see cref="Uri"/> of the HTTP request.
    /// </summary>
    public Uri? Uri => Request.RequestUri;

    /// <summary>
    ///     The <see cref="HttpContent"/> of the HTTP request.
    /// </summary>
    public HttpContent? RequestBody => Request.Content;

    /// <summary>
    ///     The <see cref="HttpHeaders"/> of the HTTP request.
    /// </summary>
    public HttpRequestHeaders Headers => Request.Headers;

    /// <summary>
    ///     The timestamp of the HTTP request.
    /// </summary>
    public int RequestTimestamp { get; }

    /// <summary>
    ///     A unique identifier for the HTTP request-response pair.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnRequestExecutingEventArgs"/> class.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage"/> about to be executed by the HTTP request.</param>
    /// <param name="timestamp">The timestamp of the HTTP request.</param>
    /// <param name="guid">A unique identifier for the HTTP request-response pair.</param>
    internal OnRequestExecutingEventArgs(HttpRequestMessage request, int timestamp, Guid guid)
    {
        Request = request;
        RequestTimestamp = timestamp;
        Id = guid;
    }
}

/// <summary>
///     Represents a set of <see cref="EventArgs"/> containing information about an HTTP response received by the client.
///     This set is passed into the <see cref="Hooks.OnRequestResponseReceived"/> event handler.
/// </summary>
public class OnRequestResponseReceivedEventArgs : EventArgs
{
    /// <summary>
    ///     The <see cref="HttpResponseMessage"/> returned by the HTTP request.
    /// </summary>
    private HttpResponseMessage Response { get; } // Not publicly-exposed, used for reference only.

    /// <summary>
    ///     The <see cref="HttpStatusCode"/> of the HTTP response.
    /// </summary>
    public HttpStatusCode StatusCode => Response.StatusCode;

    /// <summary>
    ///     The <see cref="HttpMethod"/> of the HTTP request that prompted the associated response.
    /// </summary>
    public HttpMethod? Method => Response.RequestMessage?.Method;

    /// <summary>
    ///     The <see cref="Uri"/> of the HTTP request that prompted the associated response.
    /// </summary>
    public Uri? Uri => Response.RequestMessage?.RequestUri;

    /// <summary>
    ///     The <see cref="HttpHeaders"/> present in the HTTP response.
    /// </summary>
    public HttpResponseHeaders Headers => Response.Headers;

    /// <summary>
    ///     The <see cref="HttpContent"/> of the HTTP response.
    /// </summary>
    public HttpContent ResponseBody => Response.Content;

    /// <summary>
    ///     The timestamp of the HTTP request.
    /// </summary>
    public int RequestTimestamp { get; }

    /// <summary>
    ///     The timestamp of the HTTP response.
    /// </summary>
    public int ResponseTimestamp { get; }

    /// <summary>
    ///     A unique identifier for the HTTP request-response pair.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OnRequestResponseReceivedEventArgs"/> class.
    /// </summary>
    /// <param name="response">The <see cref="HttpResponseMessage"/> returned by the HTTP request.</param>
    /// <param name="requestTimestamp">The timestamp of the HTTP request.</param>
    /// <param name="responseTimestamp">The timestamp of the HTTP response.</param>
    /// <param name="guid">A unique identifier for the HTTP request-response pair.</param>
    internal OnRequestResponseReceivedEventArgs(HttpResponseMessage response, int requestTimestamp, int responseTimestamp, Guid guid)
    {
        Response = response;
        RequestTimestamp = requestTimestamp;
        ResponseTimestamp = responseTimestamp;
        Id = guid;
    }
}
