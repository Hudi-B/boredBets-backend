namespace boredBets.Repositories.Viewmodels
{
    public class FiveRaceViewModel
    {
        // Properties from Race class
        public Guid Id { get; set; }
        public DateTime RaceScheduled { get; set; }

        // Properties from Track class
        public string? Country { get; set; }
        public string? Track_Name { get; set; }

    }
}
