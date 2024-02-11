public partial class Jockey
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Quality { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
