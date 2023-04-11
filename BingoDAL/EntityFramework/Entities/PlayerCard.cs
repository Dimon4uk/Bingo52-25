using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingoDAL.EntityFramework.Entities
{
    public class PlayerCard
    {
        public int PlayerId { get; set; }
        public int CardId { get; set; }
        public Player Player { get; set; } = null!;
        public Card Card { get; set; } = null!;
        /// <summary>
        /// in real db it should be changed 
        /// via some converter or it will be in another db table
        /// </summary>
        [NotMapped]
        public List<int> SelectedNumbers { get; set; } = null!;
        /// <summary>
        /// in real db it should be changed 
        /// via some converter or it will be in another db table
        /// </summary>
        [NotMapped]
        public int[,] WinningLines { get; set; } = null!;
        public int NumberOfLines { get; set; }
    }
}
