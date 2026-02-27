namespace CDManagerBackend
{
    public class CD : IComparable<CD>

    {
        public string Performer {  get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        public CD()
        {
            Performer = string.Empty;
            Title = string.Empty;
            Duration = 0;
        }

        public CD(string performer, string title, int duration)
        {
            Performer = performer;
            Title = title;
            Duration = duration;
        }

        public int CompareTo(CD? other)
        {
            if (other == null) return 1;

            int performerComparison = string.Compare(
           this.Performer,
           other.Performer,
           StringComparison.Ordinal
            );

            if (performerComparison != 0)
                return performerComparison;

            return string.Compare(
                this.Title,
                other.Title,
                StringComparison.Ordinal
            );
        }

        public override string ToString()
        {
            return $"Performer: {Performer}, Title: {Title}, Duration: {Duration} min";
        }

    }
}
