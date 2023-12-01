using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ModelsTests;

public class OptionsTests : UnitTest
{
    public OptionsTests() : base("options")
    {
    }

    [Fact]
    [Testing.Properties]
    public void TestOptionsSerialization()
    {
        var options = new Options
        {
            // declare an existing option property
            Alcohol = true,
        };

        // add an unsupported option
        const string unsupportedOption = "unsupported_option";
        const int unsupportedValue = 123;
        options.AddAdditionalOption(unsupportedOption, unsupportedValue);

        // serialize the options object
        var dictionary = options.AsDictionary();

        // check that the existing option is present
        Assert.True(dictionary.ContainsKey("alcohol"));
        Assert.True((bool)dictionary["alcohol"]);

        // check that the unsupported option is present
        Assert.True(dictionary.ContainsKey(unsupportedOption));
        Assert.Equal(unsupportedValue, dictionary[unsupportedOption]);
    }
}
