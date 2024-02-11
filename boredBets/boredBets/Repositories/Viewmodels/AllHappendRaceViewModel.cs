namespace boredBets.Repositories.Viewmodels
{
    public class AllHappendRaceViewModel
    {
        // Properties from Race class
        public Guid Id { get; set; }
        public DateTime RaceScheduled { get; set; }

        // Properties from Track class
        public string Name { get; set; }

    }
}
