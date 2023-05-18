using System;
using System.Net.Http;

namespace EasyPost;

public class RequestBeforeExecutionEventArgs : EventArgs
{ 
    /// <summary>
    ///     The <see cref="HttpRequestMessage"/> about to be executed by the HTTP request.
    /// </summary>
    public HttpRequestMessage Request { get; }
    
    /// <summary>
    ///     The timestamp of the HTTP request.
    /// </summary>
    public int Timestamp { get; }
    
    /// <summary>
    ///     A unique identifier for the HTTP request-response pair.
    /// </summary>
    public Guid Guid { get; }
    
    /// <summary>
    ///     Constructs a new instance of the <see cref="RequestBeforeExecutionEventArgs"/> class.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequestMessage"/> about to be executed by the HTTP request.</param>
    /// <param name="timestamp">The timestamp of the HTTP request.</param>
    /// <param name="guid">A unique identifier for the HTTP request-response pair.</param>
    internal RequestBeforeExecutionEventArgs(HttpRequestMessage request, int timestamp, Guid guid)
    {
        Request = request;
        Timestamp = timestamp;
        Guid = guid;
    }
}

public class RequestResponseReceivedEventArgs : EventArgs
{
    /// <summary>
    ///     The <see cref="HttpResponseMessage"/> returned by the HTTP request.
    /// </summary>
    public HttpResponseMessage Response { get; }
    
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
    public Guid Guid { get; }
    
    /// <summary>
    ///     Constructs a new instance of the <see cref="RequestResponseReceivedEventArgs"/> class.
    /// </summary>
    /// <param name="response">The <see cref="HttpResponseMessage"/> returned by the HTTP request.</param>
    /// <param name="requestTimestamp">The timestamp of the HTTP request.</param>
    /// <param name="responseTimestamp">The timestamp of the HTTP response.</param>
    /// <param name="guid">A unique identifier for the HTTP request-response pair.</param>
    internal RequestResponseReceivedEventArgs(HttpResponseMessage response, int requestTimestamp, int responseTimestamp, Guid guid)
    {
        Response = response;
        RequestTimestamp = requestTimestamp;
        ResponseTimestamp = responseTimestamp;
        Guid = guid;
    }
}

public class Hooks
{
    /// <summary>
    ///     An <see cref="EventHandler{RequestBeforeExecutionEventArgs}"/> to view an HTTP request by the client prior to being sent.
    ///     Editing the <see cref="HttpRequestMessage"/> in this callback does not impact the <see cref="HttpRequestMessage"/> being executed.
    /// </summary>
    public EventHandler<RequestBeforeExecutionEventArgs>? OnRequestBeforeExecution { get; set; }
    
    /// <summary>
    ///     An <see cref="EventHandler{RequestResponseReceivedEventArgs}"/> to view an HTTP response received by the client.
    /// </summary>
    public EventHandler<RequestResponseReceivedEventArgs>? OnRequestResponseReceived { get; set; }
}
