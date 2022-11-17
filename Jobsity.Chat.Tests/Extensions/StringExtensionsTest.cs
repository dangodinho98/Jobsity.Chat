namespace Jobsity.Chat.Tests.Extensions
{
    using Jobsity.Chat.Borders.Extensions;

    public class StringExtensionsTest
    {
        [Theory]
        [InlineData(true, "/stock=aapl.us")]
        [InlineData(false, "/stock=")]
        [InlineData(false, " /stock=aapl.us ")]
        [InlineData(false, "trying /stock=aapl.us")]
        [InlineData(false, "-/stock=aapl.us")]
        public void IsValidCommandTest(bool expected, string input)
        {
            var isValid = input.IsValidCommand();
            Assert.Equal(expected, isValid);
        }
    }
}
