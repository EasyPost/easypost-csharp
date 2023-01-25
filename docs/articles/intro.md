## Configuration

A `Client` object is the entry point into the EasyPost API. It is instantiated with your API key:

```csharp
using EasyPost;

Client myClient = new Client("EASYPOST_API_KEY");
```

An API key is required for all requests. You can find your API key in
your [EasyPost dashboard](https://easypost.com/account/api-keys).

Once declared, a client's API key cannot be changed. If you are using multiple API keys, you can create multiple client
objects.

### Services

All general API services can be accessed through the `Client` object. For example, to access the `Address` service:

```csharp
AddressService addressService = myClient.Address;
```

Beta services can be accessed via the `myClient.Beta` property.

```csharp
ExampleService betaService = myClient.Beta.Example;
```

### Resources

API objects cannot be created locally. All local objects are copies of server-side data, retrieved via an API call from
a service.

For example, to create a new shipment, you must use the client's Shipment service:

```csharp
Shipment myShipment = await myClient.Shipment.Create(new Dictionary<string, object>
{
    { "from_address", fromAddress },
    { "to_address", toAddress },
    { "parcel", parcel }
});
```

Functions involving a specific resource are then enacted on that resource. For example, to buy the shipment:

```csharp
await myShipment.Buy(myShipment.LowestRate());
```

Any generated local resource will have stored internally the same client used to create or retrieve them. Any API call
made against the resource will automatically use the same client. This will prevent potential issues of accidentally
using the wrong API key when interacting with a resource in a multi-client environment.


## Upgrading

Upgrading major versions of this project? Refer to the [Upgrade Guide](https://github.com/EasyPost/easypost-csharp/blob/master/UPGRADE_GUIDE.md).
