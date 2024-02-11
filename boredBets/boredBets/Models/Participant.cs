using boredBets.Models;
using System.Text.Json.Serialization;

public partial class Participant
{
    public Guid Id { get; set; }
    public Guid? RaceId { get; set; }
    public Guid? HorseId { get; set; }
    public Guid? JockeyId { get; set; }
    public int? Placement { get; set; }
    [JsonIgnore]
    public virtual Horse Horse { get; set; }
    [JsonIgnore]
    public virtual Jockey Jockey { get; set; }
    [JsonIgnore]
    public virtual Race Race { get; set; }
}
