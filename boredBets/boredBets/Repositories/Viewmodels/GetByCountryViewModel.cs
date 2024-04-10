namespace boredBets.Repositories.Viewmodels
{
    public class GetByCountryViewModel
    {
        public Guid Id { get; set; }
        public string? Country { get; set; }
        public float? Length { get; set; }
        public bool? Rain { get; set; }
    }
}
