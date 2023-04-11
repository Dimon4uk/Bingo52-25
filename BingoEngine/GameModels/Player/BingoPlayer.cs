using BingoCore.GameModels.Interfaces;
using System.Collections.Concurrent;

namespace BingoCore.GameModels.Player
{
    public class BingoPlayer : IPlayer
    {
        public int Id { get; set; }
        public ConcurrentBag<int> SelectedNumbers { get; set; } = null!;
        public int[,] WinningLines { get; set; } = null!;

        public int WinLineCount { get; set; } = 0;
    }
}
