namespace Jobsity.Chat.Bot.Mappers
{
    using CsvHelper.Configuration;
    using Jobsity.Chat.Borders.Dto;
    using Jobsity.Chat.Bot.Converters;

    public sealed class SymbolDtoMapper : ClassMap<SymbolDto>
    {
        public SymbolDtoMapper()
        {
            Map(m => m.Symbol).Index(0);
            Map(m => m.Close).Index(6).TypeConverter<DecimalConverter>();
        }
    }
}
