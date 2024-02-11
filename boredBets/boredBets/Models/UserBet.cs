using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace boredBets.Models
{
    public partial class UserBet
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public Guid? RaceId { get; set; }

        public Guid? HorseId { get; set; }

        public float? BetAmount { get; set; }

        [JsonIgnore]
        public virtual Horse? Horse { get; set; }

        [JsonIgnore]
        public virtual Race? Race { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
