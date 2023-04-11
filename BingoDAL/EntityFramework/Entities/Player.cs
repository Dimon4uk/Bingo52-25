using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingoDAL.EntityFramework.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public ICollection<PlayerCard> PlayerCards { get; set; } = null!;
    }
}
