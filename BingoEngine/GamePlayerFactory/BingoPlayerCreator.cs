using BingoCore.GameModels.Interfaces;
using BingoCore.GameModels.Player;
using BingoCore.GameParameters.Interfaces;
using BingoCore.GamePlayerFactory.Interfaces;
using System.Collections.Concurrent;
namespace BingoCore.GamePlayerFactory
{
    public class BingoPlayerCreator : IPlayerCreator
    {
        public IPlayer Create(IGameParameters gameParameters)
        {
            return new BingoPlayer
            {
                SelectedNumbers = new ConcurrentBag<int>(),
                WinningLines = new int[gameParameters.Size, gameParameters.Size]
            };
        }
    }
}
