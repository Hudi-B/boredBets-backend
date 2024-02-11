using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boredBets.Models
{
    public class UserCard
    {
        [Key]
        public int CreditcardNum { get; set; }

        public int Cvc { get; set; }

        public string ExpDate { get; set; }

        public string CardName { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
