using BingoCore.GameModels.Interfaces;

namespace BingoCore.GameEngine.Interfaces
{
    public interface IWinningLineCalculator
    {
        (int[,] winLineArray, int lines) Calculate(ICard card, IPlayer player);
    }
}
