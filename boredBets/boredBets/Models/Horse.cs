using boredBets.Models;

public partial class Horse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public bool? Stallion { get; set; }

    public virtual ICollection<UserBet> UserBets { get; set; } = new List<UserBet>();
    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
