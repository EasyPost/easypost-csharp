namespace EasyPost.Tests._Utilities.Assertions
{
    public abstract partial class Assert
    {
        public static void Pass()
        {
            // Assert something that is always true
            Xunit.Assert.True(true);
        }
    }
}
