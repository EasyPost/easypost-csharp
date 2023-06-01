using System;
using System.Net.Http;

namespace EasyPost;

public class Hooks
{
    /// <summary>
    ///     A <see cref="Action{HttpRequestMessage}"/> to view an HTTP request by the client prior to being sent.
    ///     The <see cref="HttpRequestMessage"/> about to be executed by the client is passed into this action.
    ///     Editing the <see cref="HttpRequestMessage"/> in this action does not impact the <see cref="HttpRequestMessage"/> being executed (passed by value, not reference).
    /// </summary>
    public Action<HttpRequestMessage>? PreRequestAuditor { get; set; }
    
    /// <summary>
    ///     A <see cref="Func{HttpRequestMessage, HttpRequestMessage}"/> to view and edit an HTTP request by the client prior to being sent.
    ///     The <see cref="HttpRequestMessage"/> about to be executed by the client is passed into this action.
    ///     The <see cref="HttpRequestMessage"/> returned by this action is the <see cref="HttpRequestMessage"/> that will be executed by the client.
    /// </summary>
    public Func<HttpRequestMessage, HttpRequestMessage>? PreRequestEditor { get; set; }
    
    /// <summary>
    ///     A <see cref="Action{HttpResponseMessage}"/> to view an HTTP response received by the client.
    ///     The <see cref="HttpResponseMessage"/> received by the client is passed into this action.
    /// </summary>
    public Action<HttpResponseMessage>? PostRequestAuditor { get; set; }
    
    /// <summary>
    ///     A <see cref="Action{HttpResponseMessage}"/> to view an HTTP error received by the client.
    ///     The <see cref="HttpResponseMessage"/> received by the client is passed into this action.
    /// </summary>
    public Action<HttpResponseMessage>? OnHttpError { get; set; }  // TODO: Not currently used, but could be useful in the future
}
