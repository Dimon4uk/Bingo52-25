using BingoCore.GameCardFactory.Interfaces;
using BingoCore.GameEngine.Interfaces;
using BingoCore.GameModels.Interfaces;
using BingoCore.GameParameters.Interfaces;
using BingoCore.GamePlayerFactory.Interfaces;
using BingoCore.Helpers;

namespace BingoCore.GameEngine
{
    public class BingoGameEngine : IGameEngine
    {
        private readonly IWinningLineCalculator _calculator;
        private readonly ICardCreator _cardCreator;
        private readonly IPlayerCreator _playerCreator;

        public BingoGameEngine(
            IWinningLineCalculator calculator,
            ICardCreator cardCreator, 
            IPlayerCreator playerCreator)
        {
            _calculator = calculator;
            _cardCreator = cardCreator;
            _playerCreator = playerCreator;
        }

        public (ICard card, IPlayer player) CreatePlayerGame(IGameParameters gameParameters) 
        {
            var player = _playerCreator.Create(gameParameters);
            var card = _cardCreator.Create(gameParameters);
            return (card, player);
        }
        public (int[,] winLineArray, int lines) CalculatePlayerWinLines(ICard card, IPlayer player)
        {
            return _calculator.Calculate(card, player);
        }

        public int GenerateNextNumber(IPlayer player, IGameParameters gameParameters)
        {
            int nextNumber = RandomHelper.GenerateNextWithSkip(
               gameParameters.MinValue,
               gameParameters.MaxValue,
               player.SelectedNumbers);

            return nextNumber;
        }
    }
}