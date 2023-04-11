using BingoCore.GameModels.Interfaces;
using BingoCore.GameParameters.Interfaces;

namespace BingoCore.GameEngine.Interfaces
{
    public interface IGameEngine
    {
        (ICard card, IPlayer player) CreatePlayerGame(IGameParameters gameParameters);
        int GenerateNextNumber(IPlayer player, IGameParameters gameParameters);
        (int[,] winLineArray, int lines) CalculatePlayerWinLines(ICard card, IPlayer player);
    }
}
