namespace Jobsity.Chat.Tests.Extensions
{
    using Jobsity.Chat.Borders.Extensions;

    public class DateTimeExtensionsTest
    {
        [Fact]
        public void ToFriendlyDateStringTodayTest()
        {
            var date = DateTime.Today.AddHours(10);

            var dateString = date.ToFriendlyDateString();
            Assert.Equal("Today, at 10:00", dateString);
        }

        [Fact]
        public void ToFriendlyDateStringYesterdayTest()
        {
            var date = DateTime.Today.AddDays(-1).AddHours(10);

            var dateString = date.ToFriendlyDateString();
            Assert.Equal("Yesterday, at 10:00", dateString);
        }
    }
}
