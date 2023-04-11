using BingoCore.GameModels.Interfaces;
using BingoCore.GameParameters.Interfaces;

namespace BingoCore.GamePlayerFactory.Interfaces
{
    public interface IPlayerCreator
    {
        IPlayer Create(IGameParameters gameParameters);
    }
}
