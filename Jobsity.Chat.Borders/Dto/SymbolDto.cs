namespace Jobsity.Chat.Borders.Dto
{
    using CsvHelper.Configuration.Attributes;

    public class SymbolDto
    {
        [Index(0)]
        public string Symbol { get; set; }
        [Index(1)]
        public DateTime Date { get; set; }
        [Index(2)]
        public DateTime Time { get; set; }
        [Index(3)]
        public decimal Open { get; set; }
        [Index(4)]
        public decimal High { get; set; }
        [Index(5)]
        public decimal Low { get; set; }
        [Index(6)]
        public decimal Close { get; set; }
        [Index(7)]
        public int Volume { get; set; }
    }
}