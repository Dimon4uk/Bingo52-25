using BingoCore.GameCardFactory.Interfaces;
using BingoCore.GameModels.Card;
using BingoCore.Helpers;
using BingoCore.GameModels.Interfaces;
using BingoCore.GameParameters.Interfaces;

namespace BingoCore.GameCardFactory
{
    public class BingoCardCreator : ICardCreator
    {
        public ICard Create(IGameParameters gameParameters)
        {
                return new BingoCard(
                    gameParameters.Size,
                    RandomHelper.GenerateRandomUniqueArray(
                                gameParameters.Size,
                                gameParameters.Size,
                                gameParameters.MinValue,
                                gameParameters.MaxValue));
                        
        }
    }
}

