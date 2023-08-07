using EasyPost.Models.API;
using EasyPost.Parameters.Parcel;
using Xunit;
using System.Web.Mvc;
using EasyPost.Integration.Utilities;
using EasyPost.Integration.Utilities.Attributes;

namespace EasyPost.Integration;

public class Synchronous
{
    private Utils.VCR Vcr { get; } = new("synchronous", Utils.ApiKey.Test);

    /// <summary>
    ///     Test that an end-user can run asynchronous code asynchronously
    /// </summary>
    [Fact, Testing.Run]
    public async void TestUserCanRunAsyncCodeAsynchronously()
    {
        var client = Vcr.SetUpTest("async");

        // create a parcel asynchronously
        var parcel = await client.Parcel.Create(new Create
        {
            Height = 1,
            Length = 1,
            Width = 1,
            Weight = 1,
        });
        Assert.NotNull(parcel);
        Assert.IsType<Parcel>(parcel);

        string parcelId = parcel.Id!;

        // retrieve a parcel asynchronously
        var retrievedParcel = await client.Parcel.Retrieve(parcelId);
        Assert.NotNull(retrievedParcel);
        Assert.IsType<Parcel>(retrievedParcel);
    }

    /// <summary>
    ///     Test that an end-user can run asynchronous code synchronously via .Result
    /// </summary>
    [Fact, Testing.Run]
    public void TestUserCanRunAsyncCodeSynchronouslyViaResult()
    {
        var client = Vcr.SetUpTest("via_result");

        // create a parcel via .Result
        var parcel = client.Parcel.Create(new Create
        {
            Height = 1,
            Length = 1,
            Width = 1,
            Weight = 1,
        }).Result;
        Assert.NotNull(parcel);
        Assert.IsType<Parcel>(parcel);

        string parcelId = parcel.Id!;

        // retrieve a parcel via .Result
        var retrievedParcel = client.Parcel.Retrieve(parcelId).Result;
        Assert.NotNull(retrievedParcel);
        Assert.IsType<Parcel>(retrievedParcel);
    }

    /// <summary>
    ///     Test that an end-user can run asynchronous code synchronously via .GetAwaiter().GetResult()
    /// </summary>
    [Fact, Testing.Run]
    public void TestUserCanRunAsyncCodeSynchronouslyViaGetAwaiter()
    {
        var client = Vcr.SetUpTest("via_get_awaiter");

        // create a parcel via GetAwaiter().GetResult()
        var parcel = client.Parcel.Create(new Create
        {
            Height = 1,
            Length = 1,
            Width = 1,
            Weight = 1,
        }).GetAwaiter().GetResult();
        Assert.NotNull(parcel);
        Assert.IsType<Parcel>(parcel);

        string parcelId = parcel.Id!;

        // retrieve a parcel via GetAwaiter().GetResult()
        var retrievedParcel = client.Parcel.Retrieve(parcelId).GetAwaiter().GetResult();
        Assert.NotNull(retrievedParcel);
        Assert.IsType<Parcel>(retrievedParcel);
    }
}

#pragma warning disable CA3147 // Mark Verb Handlers With Validate Antiforgery Token
/// <summary>
///     Test that an end-user can run asynchronous code in System.Web.Mvc.Controller
/// </summary>
public class SynchronousMvcController : System.Web.Mvc.Controller
{
    private Utils.VCR Vcr { get; } = new("synchronous_mvc_controller", Utils.ApiKey.Test);

    /// <summary>
    ///     Test that an end-user can run asynchronous code asynchronously
    /// </summary>
    [Fact, Testing.Run]
    public async Task<ActionResult> TestUserCanRunAsyncCodeAsynchronously()
    {
        var client = Vcr.SetUpTest("async");

        // create a parcel asynchronously
        var parcel = await client.Parcel.Create(new Create
        {
            Height = 1,
            Length = 1,
            Width = 1,
            Weight = 1,
        });
        Assert.NotNull(parcel);
        Assert.IsType<Parcel>(parcel);

        string parcelId = parcel.Id!;

        // retrieve a parcel asynchronously
        var retrievedParcel = await client.Parcel.Retrieve(parcelId);
        Assert.NotNull(retrievedParcel);
        Assert.IsType<Parcel>(retrievedParcel);

        return new EmptyResult();
    }

    /// <summary>
    ///     Test that an end-user can run asynchronous code synchronously via TaskFactory
    ///     Ref: https://gist.github.com/leonardochaia/98ce57bcee39c18d88682424a6ffe305
    /// </summary>
    [Fact, Testing.Run]
    public ActionResult TestUserCanRunAsyncCodeSynchronouslyViaTaskFactory()
    {
        var client = Vcr.SetUpTest("via_task_factory");

        // create a parcel via TaskFactory (via AsyncHelper)
        var parcel = AsyncHelper.RunSync(() => client.Parcel.Create(new Create
        {
            Height = 1,
            Length = 1,
            Width = 1,
            Weight = 1,
        }));
        Assert.NotNull(parcel);
        Assert.IsType<Parcel>(parcel);

        string parcelId = parcel.Id!;

        // retrieve a parcel via TaskFactory (via AsyncHelper)
        var retrievedParcel = AsyncHelper.RunSync(() => client.Parcel.Retrieve(parcelId));
        Assert.NotNull(retrievedParcel);
        Assert.IsType<Parcel>(retrievedParcel);

        return new EmptyResult();
    }

    private static class AsyncHelper
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static void RunSync(Func<Task> func)
        {
            TaskFactory.StartNew<Task>(func).Unwrap().GetAwaiter().GetResult();
        }

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return TaskFactory.StartNew<Task<TResult>>(func).Unwrap<TResult>().GetAwaiter().GetResult();
        }
    }
}
#pragma warning restore CA3147 // Mark Verb Handlers With Validate Antiforgery Token
