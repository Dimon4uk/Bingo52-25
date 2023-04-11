using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingoDAL.EntityFramework.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        public int Size { get; set; }

        public PlayerCard PlayerCard { get; set; } = null!;

        /// <summary>
        /// in real db it should be changed 
        /// via some converter or it will be in another db table
        /// </summary>
        [NotMapped] 
        public int[,] CardGrid { get; set; } = null!;
       
    }
}
