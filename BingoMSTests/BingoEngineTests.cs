using BingoCore.GameCardFactory;
using BingoCore.GameCardFactory.Interfaces;
using BingoCore.GameEngine;
using BingoCore.GameEngine.Interfaces;
using BingoCore.GameModels.Card;
using BingoCore.GameModels.Interfaces;
using BingoCore.GameModels.Player;
using BingoCore.GameParameters;
using BingoCore.GameParameters.Enums;
using BingoCore.GameParameters.Interfaces;
using BingoCore.GamePlayerFactory;
using BingoCore.GamePlayerFactory.Interfaces;
using static BingoCore.GameParameters.Consts.GameConstants;

namespace BingoTests
{
    [TestClass]
    public class BingoEngineTests
    {
        private IGameEngine _gameEngine;
        private IGameParameters _gameParameters;


        [TestInitialize]
        public void TestInitialize()
        {
            IWinningLineCalculator calculator = new SingleLineCalculator();
            ICardCreator cardCreator = new BingoCardCreator();
            IPlayerCreator playerCreator = new BingoPlayerCreator();

            _gameParameters = new BingoGameParameters
                    (
                        GameCodes.BINGO_52_25,
                        BaseBingo.Size,
                        BaseBingo.MinCardValue,
                        BaseBingo.MaxCardValue
                    );

            _gameEngine = new BingoGameEngine(calculator, cardCreator, playerCreator);
        }

        [TestMethod]
        public void CheckMainGameFunctionality()
        {
            var game = _gameEngine.CreatePlayerGame(_gameParameters);
            Assert.IsNotNull(game);
            Assert.IsNotNull(game.card);
            Assert.IsInstanceOfType(game.card, typeof(ICard));
            
            Assert.AreEqual(game.card.CardGrid.Length, game.card.Size*game.card.Size);
            Assert.AreEqual(game.card.Size, _gameParameters.Size);

            List<int> oneDimCard = game.card.CardGrid.Cast<int>().ToList();

            Assert.AreEqual(oneDimCard.Count, game.card.Size*game.card.Size);
            Assert.AreEqual(oneDimCard.Distinct().Count(), game.card.CardGrid.Length);
            Assert.IsTrue(oneDimCard.Max() <= _gameParameters.MaxValue);
            Assert.IsTrue(oneDimCard.Min() >= _gameParameters.MinValue);

            Assert.IsNotNull(game.player);
            Assert.IsInstanceOfType(game.player, typeof(IPlayer));

            Assert.IsNotNull(game.player.SelectedNumbers);
            Assert.AreEqual(game.player.SelectedNumbers.Count, 0);
            Assert.IsNotNull(game.player.WinningLines);
            Assert.IsInstanceOfType(game.player.WinningLines, typeof(int[,]));
            Assert.AreEqual(game.player.WinningLines.GetLength(0), _gameParameters.Size);
            Assert.AreEqual(game.player.WinningLines.GetLength(1), _gameParameters.Size);

            for (int i = 0; i < _gameParameters.MaxValue; i++) 
            {
                var next = _gameEngine.GenerateNextNumber(game.player, _gameParameters);
                Assert.IsTrue(next >= _gameParameters.MinValue);
                Assert.IsTrue(next <= _gameParameters.MaxValue);
                game.player.SelectedNumbers.Add(next);
                Assert.AreEqual(i + 1, game.player.SelectedNumbers.Count);
            }

            var results = _gameEngine.CalculatePlayerWinLines(game.card, game.player);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.lines, 12);// 12 win lines should be regarding current algoritm logic
            Assert.AreEqual(results.winLineArray.Length, game.card.CardGrid.Length);
            var listMask = results.winLineArray.Cast<int>().ToList();
            int j = 0;
            foreach(var line in listMask)
            {
                Assert.AreEqual(oneDimCard[j], listMask[j]);
            }
            Assert.AreEqual(listMask.Count, game.card.Size * game.card.Size);
            Assert.AreEqual(listMask.Distinct().Count(), game.card.CardGrid.Length);
            Assert.IsTrue(oneDimCard.Max() <= _gameParameters.MaxValue);
            Assert.IsTrue(oneDimCard.Min() >= _gameParameters.MinValue);

        }

    }
}