using EasyPost;

EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY");

Insurance insurance = await Insurance.Create(new Dictionary<string, object>() {
    { "to_address", new Dictionary<string, object>()
        {
            {
                "id", "adr_..."
            }
        }
    },
    {
        "from_address", new Dictionary<string, object>()
        {
            {
                "id", "adr_..."
            }
        }
    },
    { "reference", "InsuranceRef1" },
    { "carrier", "USPS" },
    { "tracking_code", "9400110898825022579493" },
    { "amount", "100.00" }
});

    