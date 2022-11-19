namespace Jobsity.Chat.Bot.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using System.Globalization;

    public class DecimalConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : -1;
        }
    }
}
