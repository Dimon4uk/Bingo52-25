using BingoCore.GameModels.Interfaces;
using BingoCore.GameParameters.Interfaces;

namespace BingoCore.GameCardFactory.Interfaces
{
    public interface ICardCreator
    {
        ICard Create(IGameParameters gameParameters);
    }
}
