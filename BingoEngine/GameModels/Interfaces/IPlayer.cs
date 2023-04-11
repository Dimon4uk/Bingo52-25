using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingoCore.GameModels.Interfaces
{
    public interface IPlayer
    {
        public int Id { get; }
        ConcurrentBag<int> SelectedNumbers { get; }
        int[,] WinningLines { get; }
        int WinLineCount { get; }
    }
}
