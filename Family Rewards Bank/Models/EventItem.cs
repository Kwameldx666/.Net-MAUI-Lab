namespace Family_Rewards_Bank.Models
{
    public class EventItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            string result = $"{Date:d} {Time:hh\\:mm} - {Description}";
            if (!string.IsNullOrWhiteSpace(Location))
                result += $" @ {Location}";
            return result;
        }
    }
}